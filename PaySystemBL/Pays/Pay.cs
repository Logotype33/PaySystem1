using PaySystemBL.Pays.PaysInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Pays
{
    public class Pay : IPay
    {
        private readonly IPayInfo _info;
        public Pay(IPayInfo info)
        {
            _info = info;
        }
        
        void IPay.Pay()
        {
            try
            {
                DbConnection.Open();
                SqlTransaction trans = DbConnection.connection.BeginTransaction();
                DbConnection.cmd.Transaction = trans;
                DbConnection.cmd.Parameters.Clear();
                int res = 0;
                SqlParameter returnValue;
                foreach (var orderItem in _info.GetPayItemId())
                {
                    Guid guid = Guid.NewGuid();
                    foreach (var score in _info.GetIdOfScore())
                    {
                        DbConnection.cmd.Parameters.Clear();
                        DbConnection.cmd.Parameters.AddWithValue("@usId", _info.GetPayerId());
                        DbConnection.cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                        DbConnection.cmd.Parameters.AddWithValue("@idOfScore", score);
                        DbConnection.cmd.Parameters.AddWithValue("@transactionId", guid);
                        DbConnection.cmd.CommandText = "insert into pays (sum_of_pay,user_id,product_id,payer_score_id,transaction_id) values(" +
                            "(select balance_sum from unitofscore where id=@idOfScore),@usId,@orderItemId,@idOfScore,@transactionId)";
                        DbConnection.cmd.ExecuteNonQuery();
                        
                    }
                    DbConnection.cmd.CommandType = CommandType.StoredProcedure;
                    DbConnection.cmd.CommandText = "CheckPayedSum";
                    DbConnection.cmd.Parameters.Clear();
                    DbConnection.cmd.Parameters.AddWithValue("@transactionId", guid);
                    DbConnection.cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                    returnValue = DbConnection.cmd.Parameters.AddWithValue("@Return", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    DbConnection.cmd.ExecuteNonQuery();
                    res = Convert.ToInt32(returnValue.Value);
                    DbConnection.cmd.CommandType = CommandType.Text;
                    if (res == 0)
                    {
                        trans.Rollback();
                        return;
                    }

                    DbConnection.cmd.Parameters.Clear();
                    DbConnection.cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                    DbConnection.cmd.CommandText = "update orderitem set status='payed' where id=@orderItemId";
                    DbConnection.cmd.ExecuteNonQuery();
                }
                if (res == 0)
                {
                    trans.Rollback();
                }
                else if (res == 1)
                {
                    trans.Commit();
                }
                DbConnection.cmd.Parameters.Clear();
                DbConnection.Close();
            }
            catch (Exception e)
            {
                DbConnection.Close();
                throw new Exception(e.Message);
            }
        }

    }
}

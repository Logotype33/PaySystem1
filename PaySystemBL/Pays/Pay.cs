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
            SqlTransaction trans = null;
            int payId = 0;
            try
            {
                DbConnection.cmd.Parameters.Clear();
                DbConnection.Open();
                trans = DbConnection.connection.BeginTransaction();
                DbConnection.cmd.Transaction = trans;
                int res = 0;
                foreach (var orderItem in _info.GetPayItemId())
                {
                    DbConnection.cmd.Parameters.AddWithValue("@usId", _info.GetPayerId());
                    DbConnection.cmd.CommandText = "insert into pays (sum_of_pay,user_id) values(0,@usId) " +
                    "select SCOPE_IDENTITY()";
                    var read = DbConnection.cmd.ExecuteReader();
                    if (read.Read())
                    {
                        payId = Convert.ToInt32(read[0]);
                    }
                    read.Close();
                    foreach (var score in _info.GetIdOfScore())
                    {
                        DbConnection.cmd.Parameters.Clear();
                        DbConnection.cmd.CommandType = CommandType.StoredProcedure;
                        DbConnection.cmd.CommandText = "ReduceFromScore";
                        DbConnection.cmd.Parameters.AddWithValue("@idOfScore", score);
                        DbConnection.cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                        DbConnection.cmd.Parameters.AddWithValue("@userId", _info.GetPayerId());
                        DbConnection.cmd.Parameters.AddWithValue("@payId", payId);
                        var returnValue = DbConnection.cmd.Parameters.AddWithValue("@Return", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        DbConnection.cmd.ExecuteNonQuery();
                        res = Convert.ToInt32(returnValue.Value);
                        DbConnection.cmd.CommandType = CommandType.Text;
                    }
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

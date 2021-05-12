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
                DbConnection.cmd.Parameters.Clear();
                DbConnection.Open();
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
                    DbConnection.cmd.Parameters.Clear();
                    DbConnection.cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                    DbConnection.cmd.CommandText = "update orderitem set status='payed' where id=@orderItemId";
                    DbConnection.cmd.ExecuteNonQuery();
                }
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    public class Pays
    {
        readonly SqlCommand cmd = DbConnection.GetSqlCommand();
        public void PayOrder(List<int> orderItemsId,List<int> idOfScores)
        {
            SqlTransaction trans = null;
            int payId = 0;
            try
            {
                cmd.Parameters.Clear();
                DbConnection.Open();
                trans = DbConnection.connection.BeginTransaction();
                cmd.Transaction = trans;
                int res = 0;
                foreach (var orderItem in orderItemsId)
                {
                    cmd.Parameters.AddWithValue("@usId", User.Id);
                    cmd.CommandText = "insert into pays (sum_of_pay,user_id) values(0,@usId) " +
                    "select SCOPE_IDENTITY()";
                    var read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        payId = Convert.ToInt32(read[0]);
                    }
                    read.Close();
                    foreach (var score in idOfScores)
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "ReduceFromScore";
                        cmd.Parameters.AddWithValue("@idOfScore", score);
                        cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                        cmd.Parameters.AddWithValue("@userId", User.Id);
                        cmd.Parameters.AddWithValue("@payId", payId);
                        var returnValue = cmd.Parameters.AddWithValue("@Return", SqlDbType.Int);
                        returnValue.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();
                        res = Convert.ToInt32(returnValue.Value);
                        cmd.CommandType = CommandType.Text;
                    }
                    if (res == 0)
                    {
                        trans.Rollback();
                        return;
                    }
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@orderItemId", orderItem);
                    cmd.CommandText = "update orderitem set status='payed' where id=@orderItemId";
                    cmd.ExecuteNonQuery();
                }
                if (res == 0)
                {
                    trans.Rollback();
                }
                else if (res == 1)
                {
                    trans.Commit();
                }
                cmd.Parameters.Clear();
               
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

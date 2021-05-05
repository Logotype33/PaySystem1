using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    public class Orders
    {
        readonly SqlCommand cmd = DbConnection.GetSqlCommand();
        public DataTable GetOrderItems()
        {
            cmd.Parameters.AddWithValue("@usId", User.Id);
            try
             {
                DbConnection.Open();
                cmd.CommandText= "select id,name,price,status from orderItem where order_id=(select id from orders where user_id=@usId)";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                cmd.Parameters.Clear();
                DbConnection.Close();
                return table;
             }
             catch (Exception e)
             {
                 DbConnection.Close();

                throw new Exception(e.Message);
            }

        }
    }
}

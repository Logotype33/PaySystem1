using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.BalanceFolder.Get
{
    public class GetBalance:IGetBalance
    {
        double balance = 0;
        public double GetTotalBalance()
        {
            DbConnection.cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                DbConnection.cmd.CommandText = "Select SUM(Balance_sum) from UnitOfScore,Users,Balance where Users.balance_id=(SELECT balance_id FROM Users WHERE id=@usId) and "
                    + "UnitOfScore.Balance_id=Users.balance_id AND Balance.User_id=Users.Id";
                var reader1 = DbConnection.cmd.ExecuteReader();
                if (reader1.Read())
                {
                    balance = (double)reader1[0];
                }
                reader1.Close();
                DbConnection.Close();
                DbConnection.cmd.Parameters.Clear();
                return balance;
            }
            catch (Exception e)
            {
                DbConnection.Close();

                throw new Exception(e.Message);
            }
        }
        public DataTable GetTableOfScore()
        {
            if (!DbConnection.cmd.Parameters.Contains("@usId"))
                DbConnection.cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                DbConnection.cmd.CommandText = "SELECT id,balance_sum FROM UnitOfScore WHERE UnitOfScore.Balance_id=(SELECT balance_id FROM Users WHERE id=@usId)";
                SqlDataAdapter adapter = new SqlDataAdapter(DbConnection.cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                DbConnection.cmd.Parameters.Clear();
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

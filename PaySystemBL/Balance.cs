using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    public class Balance
    {
        SqlCommand cmd=DbConnection.GetSqlCommand();
        UnitOfScore score = new UnitOfScore();
        double balance =0;

        public Balance()
        {

        }
        public double ShowTotalBalance()
        {
            cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                cmd.CommandText = "Select SUM(Balance_sum) from UnitOfScore,Users,Balance where Users.balance_id=(SELECT balance_id FROM Users WHERE id=@usId) and " 
                    + "UnitOfScore.Balance_id=Users.balance_id AND Balance.User_id=Users.Id";
                var reader1 = cmd.ExecuteReader();
                if (reader1.Read())
                {
                    balance = (double)reader1[0];
                }
                reader1.Close();
                DbConnection.Close();
                cmd.Parameters.Clear();
                return balance;
            }
            catch (Exception)
            {
                DbConnection.Close();

                throw;
            }
        }
        public DataTable ShowUnitOfScore()
        {
            if(!cmd.Parameters.Contains("@usId"))
                cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                cmd.CommandText = "SELECT id,balance_sum FROM UnitOfScore WHERE UnitOfScore.Balance_id=(SELECT balance_id FROM Users WHERE id=@usId)";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                cmd.Parameters.Clear();
                DbConnection.Close();
                return table;
            }
            catch (Exception)
            {
                DbConnection.Close();

                throw;
            }
        }
        public void CreateNewBalance()
        {
            cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                cmd.CommandText = "insert into Balance (User_id) values (@usId)";
                cmd.ExecuteNonQuery();
                DbConnection.Close();
            }
            catch (Exception)
            {
                DbConnection.Close();
                throw;
            }

        }
        public void CreateNewScore(float paySum)
        {
            cmd.Parameters.AddWithValue("@usId", User.Id);
            cmd.Parameters.AddWithValue("@paySum", paySum);
            try
            {
                //SELECT balance_sum FROM UnitOfScore WHERE UnitOfScore.Balance_id = (SELECT balance_id FROM Users WHERE id = @usId)
                DbConnection.Open();
                cmd.CommandText = "insert into UnitOfScore (balance_id,balance_sum) values ((select balance_id from users where id=@usId),@paySum)";
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                DbConnection.Close();
            }
            catch (Exception)
            {
                DbConnection.Close();
                throw;
            }
        }
    }
}

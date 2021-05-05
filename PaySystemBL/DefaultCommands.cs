using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    //TODO: rename and split this
    public class DefaultCommands
    {
        SqlCommand cmd;
        Balance balance = new Balance();
        public bool isRegistred=false;
        public DefaultCommands()
        {
            cmd = DbConnection.GetSqlCommand();

        }
        public void Login(string login,string password)
        {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);
            try
            {
                DbConnection.Open();
                cmd.CommandText = "Select id from Users where login=@login and password=@password";
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    User user = new User((int)reader[0],true);
                }
                else
                {
                }
                cmd.Parameters.Clear();
                DbConnection.Close();
            }
            catch (Exception)
            {
                DbConnection.Close();
                throw;
            }
            finally
            {
                DbConnection.Close();
            }
            
        }

        public void Register(string login, string password)
        {
            
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@password", password);
            try
            {
                DbConnection.Open();
                cmd.CommandText = "if (SELECT COUNT(login) FROM Users WHERE login=@login) = 0" +
                    "INSERT INTO Users (login,password) VALUES (@login,@password)" +
                    "select login from Users where login=@login";
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    isRegistred = true;
                }
                else
                {
                    isRegistred = false;
                }
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

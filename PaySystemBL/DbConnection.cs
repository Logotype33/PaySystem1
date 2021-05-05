using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PaySystemBL
{
    public class DbConnection
    {
        //Продумать какие поля должны быть статическими
        public static SqlConnection connection;
        public static SqlCommand cmd;
        public DbConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=GEORGY-PC\SQLEXPRESS;Initial Catalog=wfApp;Integrated Security=True";
            cmd = new SqlCommand();
            cmd.Connection = connection;
        }
        public static void ConnectTo(string conStr)
        {
            connection.ConnectionString = conStr;
        }
        public static void Open()
        {
            if(connection.State==System.Data.ConnectionState.Closed)
            connection.Open();
            
        }
        public static void Close()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public static SqlConnection GetConnectionString()
        {
            return connection;
        }
        public static SqlCommand GetSqlCommand()
        {
            return cmd;
        }
    }
}

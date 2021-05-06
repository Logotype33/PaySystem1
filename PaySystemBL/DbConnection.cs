using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PaySystemBL
{
    public static class DbConnection
    {
        //Продумать какие поля должны быть статическими
        public static SqlConnection connection;
        public static SqlCommand cmd;
         static DbConnection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=PaySystem1.mssql.somee.com;Initial Catalog=PaySystem1;Persist Security Info=True;User ID=logo3112_SQLLogin_1;Password=fdzdhkgze7";
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.BalanceFolder.ChangeBalance
{
    public class CreateBalance:ICreateBalance
    {
        public void CreateNewBalance()
        {
            DbConnection.cmd.Parameters.AddWithValue("@usId", User.Id);
            try
            {
                DbConnection.Open();
                DbConnection.cmd.CommandText = "insert into Balance (User_id) values (@usId)";
                DbConnection.cmd.ExecuteNonQuery();
                DbConnection.Close();
            }
            catch (Exception e)
            {
                DbConnection.Close();
                throw new Exception(e.Message);
            }

        }
        public void CreateNewScore(float paySum)
        {
            DbConnection.cmd.Parameters.AddWithValue("@usId", User.Id);
            DbConnection.cmd.Parameters.AddWithValue("@paySum", paySum);
            try
            {
                //SELECT balance_sum FROM UnitOfScore WHERE UnitOfScore.Balance_id = (SELECT balance_id FROM Users WHERE id = @usId)
                DbConnection.Open();
                DbConnection.cmd.CommandText = "insert into UnitOfScore (balance_id,balance_sum) values ((select balance_id from users where id=@usId),@paySum)";
                DbConnection.cmd.ExecuteNonQuery();
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

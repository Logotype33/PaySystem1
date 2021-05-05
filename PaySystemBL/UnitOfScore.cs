using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL
{
    public class UnitOfScore
    {
        SqlCommand cmd;
        public UnitOfScore()
        {
            cmd = DbConnection.GetSqlCommand();

        }

    }
}

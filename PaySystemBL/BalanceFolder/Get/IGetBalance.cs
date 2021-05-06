using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.BalanceFolder.Get
{
    public interface IGetBalance
    {
        double GetTotalBalance();
        DataTable GetTableOfScore();
    }
}

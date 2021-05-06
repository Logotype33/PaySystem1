using PaySystemBL.BalanceFolder.ChangeBalance;
using PaySystemBL.BalanceFolder.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.BalanceFolder
{
    public interface IBalance
    {
        ICreateBalance CreateBalance { get; set; }
        IGetBalance GetBalance { get; set; }
}
}

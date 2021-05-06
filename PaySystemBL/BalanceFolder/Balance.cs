using PaySystemBL.BalanceFolder.ChangeBalance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaySystemBL.BalanceFolder.Get;

namespace PaySystemBL.BalanceFolder
{
    public class Balance : IBalance
    {
        public ICreateBalance CreateBalance { get; set; } = new CreateBalance();
        public IGetBalance GetBalance { get; set; } = new GetBalance();
    }
}

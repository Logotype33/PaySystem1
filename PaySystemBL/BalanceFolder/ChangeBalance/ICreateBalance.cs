using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.BalanceFolder.ChangeBalance
{
    public interface ICreateBalance
    {
        void CreateNewBalance();
        void CreateNewScore(float paySum);
    }
}

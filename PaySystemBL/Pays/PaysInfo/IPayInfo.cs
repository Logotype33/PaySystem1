using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Pays.PaysInfo
{
    public interface IPayInfo
    {
       
        int GetPayerId ();
        IEnumerable<int> GetIdOfScore();
        IEnumerable<int> GetPayItemId();

    }
}

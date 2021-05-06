using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Pays.PaysInfo
{
    //Сделал получение ид счёта и товара коллекциями, т.к. это более гибко, можно в список передать 1 ид, можно несколько. 
    public class PayInfo:IPayInfo
    {
        public IEnumerable<int> _idOfScore;
        public int _payerId;
        public IEnumerable<int> _payItemId;
        public PayInfo(int payerId,List<int> payItemsId, List<int> idOfPays)
        {
            _payerId = payerId;
            _payItemId = payItemsId;
            _idOfScore = idOfPays;
        }

        public IEnumerable<int> GetIdOfScore()
        {
            return _idOfScore;
        }

        public int GetPayerId()
        {
            return _payerId;
        }

        public IEnumerable<int> GetPayItemId()
        {
            return _payItemId;
        }
    }
}

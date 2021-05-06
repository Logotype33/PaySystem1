
using PaySystemBL.BalanceFolder;
using PaySystemBL.Orders;
using PaySystemBL.Orders.GetOrders;
using PaySystemBL.Pays;
using PaySystemBL.Pays.PaysInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Service
{
    /// <summary>
    /// Сервис, объединяющий в себе реализацию функционала оплаты.
    /// </summary>
    public class PayService
    {
        //Реализация по умолчанию
        public IOrder Order { get; set; } = new Order();
        /// <summary>
        /// Для использования нужно явно указать реализацию интерфейса, она не указана по умолчанию, т.к. имеет конструктор с параметрами.
        /// </summary>
        public IPay Pay { get; set; }
        public IBalance Balance { get; set; } = new Balance();
        public PayService()
        {
            Order.GetOrder = new GetOrder();
            
        }
        public void BalanceReset()
        {
            Balance = new Balance();
        }
    }
}

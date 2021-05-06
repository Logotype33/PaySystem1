using PaySystemBL.Orders.GetOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySystemBL.Orders
{
    public interface IOrder
    {
        IGetOrder GetOrder { get; set; }
    }
}

using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOrderRepo
    {
        public List<Order> GetOrders();

        public List<Order> GetOrderByCustomerId(int customerId);
    }
}

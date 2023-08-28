using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OrderRepo : IOrderRepo
    {
        public List<Order> GetOrderByCustomerId(int customerId) => OrderDAO.Instance.GetOrderByCustomerId(customerId);

        public List<Order> GetOrders() => OrderDAO.Instance.GetOrders();
    }
}

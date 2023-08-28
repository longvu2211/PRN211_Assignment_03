using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class OrderDAO
    {
        private OrderDAO() { }
        private static readonly object instanceLock = new object();
        private static OrderDAO instance;
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Order> GetOrders()
        {
            try
            {
                List<Order> orders = null;
                var context = new FUFlowerBouquetManagementV4Context();
                orders = context.Orders.Include(ord => ord.Customer).ToList();
                return orders;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetOrderByCustomerId(int customerId)
        {
            try
            {
                List<Order> orders = null;
                var context = new FUFlowerBouquetManagementV4Context();
                orders = context.Orders.Where(ord => ord.CustomerId == customerId).ToList();
                return orders;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

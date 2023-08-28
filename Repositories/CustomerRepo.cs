using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        public Customer CheckLogin(string email, string password)
            => CustomerDAO.Instance.CheckLogin(email, password);

        public void CreateACustomer(Customer customer)
            => CustomerDAO.Instance.CreateACustomer(customer);

        public void DeleteCustomer(int id)
            => CustomerDAO.Instance.DeleteACustomer(id);
        public List<Customer> GetAllCustomers()
            => CustomerDAO.Instance.GetAllCustomers();

        public Customer GetCustomerById(int id)
            => CustomerDAO.Instance.GetCustomerById(id);

        public void UpdateCustomer(Customer customer)
            => CustomerDAO.Instance.UpdateACustomer(customer);
    }
}

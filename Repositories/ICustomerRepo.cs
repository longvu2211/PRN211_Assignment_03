using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICustomerRepo
    {
        public Customer CheckLogin(string email, string password);

        public List<Customer> GetAllCustomers();

        public Customer GetCustomerById(int id);

        public void CreateACustomer(Customer customer);

        public void UpdateCustomer(Customer customer);  

        public void DeleteCustomer(int id);
    }
}

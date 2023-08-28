using BusinessObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        private CustomerDAO() { }   
        private static readonly object instanceLock = new object();
        private static CustomerDAO instance;
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                    return instance;
                }
            }
        }

        public Customer GetAdminAccount()
        {
            try
            {
                Customer admin = null;
                IConfiguration config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
                var adEmail = config["AdminAccount:email"];
                var adPassword = config["AdminAccount:password"];
                var adRole = config["AdminAccount:role"];

                admin = new Customer()
                {
                    CustomerId = 0,
                    CustomerName = adRole,
                    Email = adEmail,
                    Password = adPassword,
                };
                return admin;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                List<Customer> customers = null;
                var context = new FUFlowerBouquetManagementV4Context();
                customers = context.Customers.ToList();
                return customers;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private List<Customer> GetCustomersWithAdmin()
        {
            try
            {
                List<Customer> customers = null;
                var context = new FUFlowerBouquetManagementV4Context();
                customers = context.Customers.ToList();
                customers = customers.Prepend(GetAdminAccount()).ToList();
                return customers;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer CheckLogin(string email, string password)
        {
            try
            {
                Customer customer = null;
                var context = GetCustomersWithAdmin();
                customer = context.SingleOrDefault(cus => cus.Email.Equals(email) && cus.Password.Equals(password));
                return customer;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                Customer cus = null;
                var context = new FUFlowerBouquetManagementV4Context();
                cus = context.Customers.FirstOrDefault(c => c.CustomerId == id);
                return cus;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateACustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new Exception("Customer is undefined!");
            }
            var context = new FUFlowerBouquetManagementV4Context();
            var emailValidation = new EmailAddressAttribute();
            var isValidate = emailValidation.IsValid(customer.Email);
            if (!isValidate)
            {
                throw new Exception("Email is invalid!");
            }
            var checkDuplicate = context.Customers.FirstOrDefault(cus => cus.CustomerId == customer.CustomerId);
            if (checkDuplicate != null)
            {
                throw new Exception("User existed!");
            }
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void UpdateACustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new Exception("Customer is undefined!");
            }
            var context = new FUFlowerBouquetManagementV4Context();
            context.Customers.Update(customer);
            context.SaveChanges();
            
        }

        public void DeleteACustomer(int id)
        {
            var context = new FUFlowerBouquetManagementV4Context();
            var deletedCustomer = context.Customers.Find(id);
            if (deletedCustomer != null)
            {
                deletedCustomer.CustomerId = 0;
                context.SaveChanges();  
            }
        }
    }
}

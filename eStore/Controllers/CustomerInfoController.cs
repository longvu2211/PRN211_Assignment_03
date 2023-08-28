using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;

namespace eStore.Controllers
{
    public class CustomerInfoController : Controller
    {
        ICustomerRepo _cusRepo = new CustomerRepo();    
        IOrderRepo _orderRepo = new OrderRepo();

        public IActionResult ShowInfo()
        {
            var session = HttpContext.Session;
            if (session.GetString("Customer") == null && session.GetString("Role") != "Cus")
            {
                return RedirectToAction("Login", "Authen");
            }
            var loginCustomer = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("Customer"));
            ViewData["Customer"] = loginCustomer;
            return View();
        }

        public IActionResult ShowDetailInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _cusRepo.GetCustomerById((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        
        public IActionResult OrdCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _orderRepo.GetOrderByCustomerId((int)id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}

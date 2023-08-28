using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;

namespace eStore.Controllers
{
    public class AuthenController : Controller
    {
        ICustomerRepo _cusRepo = new CustomerRepo();
        private static readonly string ADMIN = "Admin";
        public IActionResult Login([FromForm] string email, [FromForm] string password)
        {
            if (email == null && password == null) 
            {
                return View(); 
            }
            Customer loginCustomer = _cusRepo.CheckLogin(email, password);
            if (loginCustomer != null) 
            {
                if (ADMIN.Equals(loginCustomer.CustomerName))
                {
                    // move to admin page
                    var customer = JsonConvert.SerializeObject(loginCustomer);
                    HttpContext.Session.SetString("AdminAccount", customer);
                    HttpContext.Session.SetString("Role", "Admin");
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    var customer = JsonConvert.SerializeObject(loginCustomer);
                    HttpContext.Session.SetString("Customer", customer);
                    HttpContext.Session.SetString("Role", "Cus");
                    return RedirectToAction("ShowInfo", "CustomerInfo");
                }
            } 
            else 
            {
                ViewData["ErrorMsg"] = "Incorrect email or password!";
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Authen");
        }
    }
}

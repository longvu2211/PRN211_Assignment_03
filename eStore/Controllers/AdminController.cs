using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories;
using System.Collections.Generic;

namespace eStore.Controllers
{
    public class AdminController : Controller
    {
        ICustomerRepo _cusRepo = new CustomerRepo();
        public IActionResult Admin()
        {
            var session = HttpContext.Session;
            if (session.GetString("AdminAccount") == null && session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Login", "Authen");
            }
            var admin = JsonConvert.DeserializeObject<Customer>(HttpContext.Session.GetString("AdminAccount"));
            ViewData["Admin"] = admin;
            return View();
        }
    }
}

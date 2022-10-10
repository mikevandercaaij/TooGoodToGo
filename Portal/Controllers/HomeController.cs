using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.ExtensionMethods;
using Portal.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            var role = this.User.GetRole();

            if (role == "Student")
            {
                ViewBag.Role = "Student";
                
                return View("Index", "Student");
            }
            else if (role == "CanteenEmployee")
            {
                ViewBag.Role = "CanteenEmployee";
                return View("Index");
            }
            else
            {
                ViewBag.Role = "Not logged in";
                
                return View("Index");
            }
        }
    }
}
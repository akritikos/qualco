using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EzPay.WebApp.Controllers
{
    public class CitizenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Login Get
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //Login Post

        //Logout
    }
}

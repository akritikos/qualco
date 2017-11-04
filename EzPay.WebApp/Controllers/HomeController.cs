using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzPay.WebApp.Models;
using System.Diagnostics;

namespace EzPay.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = "About";
            ViewBag.Message = "A description of the page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            ViewBag.Message = "Contact page for the portal.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

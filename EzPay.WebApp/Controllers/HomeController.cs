using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzPay.WebApp.Models;
using System.Diagnostics;
using EzPay.EmailSender;
using EzPay.Model;
using EzPay.Model.Entities;

namespace EzPay.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEzPayRepository _ctx;

        public HomeController(IEzPayRepository ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            if(_ctx.GetSet<Bill>().Any()==false)
                return RedirectToAction(nameof(HomeController.Maintenance), "Home");
            else
                return View();
        }

        public IActionResult Maintenance()
        {
            ViewBag.Title = "We are Sorry...";
            ViewBag.Message = "We are currenty not available due to maintenance.";

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

        [HttpPost]
        public async Task<IActionResult> SendForm(ContactViewModel model)
        {
            var sender = new SmtpSender("EZPayVerify", "1q2w3e4r5t6y!@#", "smtp.gmail.com", ssl: true);
            string mailbody = $"Subject={model.Subject}<BR>UserId={model.UserId}<BR>Email={model.Email}<BR>Message={model.Message}";
            sender.SetParameters("EZPayVerify@gmail.com", "EZPayVerify@gmail.com", "ezpay", $"Contact Form Input {DateTime.Now}", string.Empty, mailbody);
            await sender.Send();

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }

        public IActionResult Error() => 
            View(new ErrorViewModel
                     {
                         RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                     });
    }
}

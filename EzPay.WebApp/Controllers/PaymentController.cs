using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzPay.WebApp.Models;
using EzPay.Model.Entities;
using Microsoft.AspNetCore.Identity;
using EzPay.Model;
using Microsoft.EntityFrameworkCore;

namespace EzPay.WebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<Citizen> _userManager;
        private readonly SignInManager<Citizen> _signInManager;
        private readonly IEzPayRepository _ctx;

        public PaymentController(
            UserManager<Citizen> userManager,
            SignInManager<Citizen> signInManager,
            IEzPayRepository ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ctx = ctx;
        }

        [TempData]
        public string CitizenStatusMessage { get; set; }

        public async Task<IActionResult> Index(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new LoginViewModel
            {
                Bills = _ctx.GetSet<Bill>().Where(c => c.Id == id)
            };

            return View(model);
        }

        public IActionResult Pay(Guid id)
        {
            Payment payment = new Payment();
            payment.BillId = id;
            payment.Date = DateTime.Now;
            payment.Method = "CREDIT";

            _ctx.Add(payment);
            _ctx.SaveChanges();

           //CitizenStatusMessage = "Your payment is complete.";

            return RedirectToAction(nameof(CitizenController.Index), "Citizen");

        }
    }
}

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
using Microsoft.AspNetCore.Authorization;

namespace EzPay.WebApp.Controllers
{
    [Authorize]
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
        public string BillStatusMessage { get; set; }

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

        
        public async Task<IActionResult> Pay(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Payment payment = new Payment();
            payment.BillId = id;
            payment.Date = DateTime.Now;
            payment.Method = "CREDIT";

            _ctx.Add(payment);
            bool status= _ctx.SaveChanges();

           if (status==true)
                BillStatusMessage = "Your payment is complete.";
           else
                BillStatusMessage = "Your payment is unsuccessful.Please try again.";

            return RedirectToAction(nameof(CitizenController.Index), "Citizen");

        }

        #region Payment Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}

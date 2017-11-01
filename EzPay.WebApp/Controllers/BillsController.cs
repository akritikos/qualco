using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EzPay.Model.Entities;
using EzPay.WebApp.Models;
using EzPay.Model;

namespace EzPay.WebApp.Controllers
{
    public class BillsController : Controller
    {
        private readonly UserManager<Citizen> _userManager;
        private readonly SignInManager<Citizen> _signInManager;

        public BillsController(
            UserManager<Citizen> userManager,
            SignInManager<Citizen> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var ctx = new EzPayContext();
            var model = new LoginViewModel
            {
                //Bills = user.Bills

                Bills = ctx.Bills.Where(c => c.CitizenId==user.Id)
            };

            return View(model);

        }
    }
}

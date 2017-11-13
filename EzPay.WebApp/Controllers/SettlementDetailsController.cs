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
    public class SettlementDetailsController : Controller
    {
        private readonly UserManager<Citizen> _userManager;
        private readonly SignInManager<Citizen> _signInManager;
        private readonly IEzPayRepository _ctx;

        public SettlementDetailsController(
            UserManager<Citizen> userManager,
            SignInManager<Citizen> signInManager,
            IEzPayRepository ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ctx = ctx;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var model = new LoginViewModel
            {
                CitizenId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                County = user.County,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Bills = _ctx.GetSet<Bill>().Where(c => c.CitizenId == user.Id)
                    .Where(c => c.IsSelected == true)
                    .Include(b => b.Settlement)
                    .Include(b => b.Payment),
                Settlements = _ctx.GetSet<Settlement>().Where(c => c.CitizenId == user.Id)
                    .Include(b => b.Bills),
                SettlementTypes = _ctx.GetSet<SettlementType>().AsQueryable(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index (List<Bill> billSelected)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var yesChecked = 0; var noChecked = 0;

            for (var i = 0; i < billSelected.Count; i++)
            {
                if (billSelected[i].IsSelected == true)
                {
                    yesChecked = yesChecked + 1;
                }
                else
                {
                    noChecked = noChecked + 1;
                }
            }
            return View(billSelected);
        }

        [HttpGet]
        public IActionResult BillsInSettlement(Guid id)
        {
            var model = new LoginViewModel
            {
                Bills = _ctx.GetSet<Bill>().Where(c => c.SettlementId == id)
            };

            return View(model);
        }

    }
}

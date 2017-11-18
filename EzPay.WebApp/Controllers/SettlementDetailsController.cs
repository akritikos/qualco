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

        [TempData]
        public string SettlementStatusMessage { get; set; }


        [HttpPost]
        public async Task<IActionResult> Settle(LoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            //var model = new LoginViewModel
            //{
            //    CitizenId = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Address = user.Address,
            //    County = user.County,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Bills = _ctx.GetSet<Bill>().Where(c => c.CitizenId == user.Id)
            //        .Where(c => c.IsSelected == true)
            //        .Include(b => b.Settlement)
            //        .Include(b => b.Payment),
            //    Settlements = _ctx.GetSet<Settlement>().Where(c => c.CitizenId == user.Id)
            //        .Include(b => b.Bills),
            model.SettlementTypes = _ctx.GetSet<SettlementType>().AsQueryable();
            //};
            model.Bills = model.BillsList.Where(b => b.IsSelected == true);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitSettlement(LoginViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if(_ctx.GetSet<SettlementType>().Where(c=>c.Id == model.SettlementTypeSelected).Count()!=1 ||
                model.InstallmentsSelected==0)
            {
                throw new ApplicationException($"Incorrect settlement type.");
            }

            Settlement settlement = new Settlement();
            settlement.Id = Guid.NewGuid();
            settlement.Date = DateTime.Now;
            settlement.CitizenId = user.Id;
            settlement.TypeId = model.SettlementTypeSelected;
            settlement.Installments = model.InstallmentsSelected;
            //settlement.Bills = model.BillsList;

            _ctx.Add(settlement);
            bool status=_ctx.SaveChanges();

            foreach(var bill in model.BillsList)
            {
                if (bill.IsSelected == true)
                {
                    var upd_bill = _ctx.GetSet<Bill>().SingleOrDefault(c => c.Id == bill.Id);
                    upd_bill.SettlementId = settlement.Id;
                    status = _ctx.SaveChanges();
                }
               
            }

            

            if(status==true)
                SettlementStatusMessage = "Settlement has been requested.";
            else
                SettlementStatusMessage = "Settlement unsuccessful. Please try again.";

            return RedirectToAction(nameof(CitizenController.Index), "Citizen");

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

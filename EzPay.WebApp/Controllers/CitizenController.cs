using EzPay.Model;
using EzPay.Model.Entities;
using EzPay.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EzPay.WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class CitizenController : Controller
    {
        private readonly UserManager<Citizen> _userManager;
        private readonly SignInManager<Citizen> _signInManager;
        private readonly IEzPayRepository _ctx;

        public CitizenController(
            UserManager<Citizen> userManager,
            SignInManager<Citizen> signInManager,
            IEzPayRepository ctx)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _ctx = ctx;
        }

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string CitizenStatusMessage { get; set; }

        [HttpGet]
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
                    .Include(b => b.Settlement)
                    .Include(b => b.Payment),
                BillsList = _ctx.GetSet<Bill>().Where(c => c.CitizenId == user.Id)
                    .Include(b => b.Settlement)
                    .Include(b => b.Payment)
                    .ToList(),
                Settlements = _ctx.GetSet<Settlement>().Where(c => c.CitizenId == user.Id)
                    .Include(b => b.Bills),
                SettlementTypes = _ctx.GetSet<SettlementType>().AsQueryable(),
                NewSettlement = new Settlement {
                    Id=new Guid(),
                    Bills=new List<Bill>()
                },
                StatusMessage = CitizenStatusMessage
            };
            
            return View(model);
        }

        #region *****Login Action*****

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //Login Post
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                Citizen user = await _userManager.FindByIdAsync(model.CitizenId.ToString());
                if (user != null)
                {
                    // Cancel existing session 
                    await _signInManager.SignOutAsync();

                    // Perform the authentication 
                    Microsoft.AspNetCore.Identity.SignInResult result =
                        await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(nameof(LoginViewModel.Password), "Password is invalid.");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginViewModel.CitizenId), "Citizen ID is invalid.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            /*var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToAction(nameof(SetPassword));
            }*/

            var model = new LoginViewModel { StatusMessage = CitizenStatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            CitizenStatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(CitizenController.Index), "Citizen");
        }

        #region *****Helpers*****

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(CitizenController.Index), "Citizen");
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        #endregion

    }
}

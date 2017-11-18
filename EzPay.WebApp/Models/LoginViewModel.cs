using EzPay.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzPay.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Your Citizen ID is required.")]
        [Display(Name ="Citizen ID")]
        public long CitizenId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string County { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<Bill> Bills { get; set; }
        public List<Bill> BillsList { get; set; }
        public IEnumerable<Settlement> Settlements { get; set; }
        public IEnumerable<SettlementType> SettlementTypes { get; set; }

        public Settlement NewSettlement { get; set; }

        /*change password*/

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }

        /*...change password*/

    }
}

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
        public string County { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public IEnumerable<Bill> Bills { get; set; }
    }
}

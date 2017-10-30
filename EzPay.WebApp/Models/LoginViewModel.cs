using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EzPay.WebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Your Citizen ID is required.")]
        [Display(Name ="Citizen ID")]
        public string CitizenId { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirmation should match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

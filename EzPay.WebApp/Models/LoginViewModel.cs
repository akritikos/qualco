using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public bool RememberMe { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
    }
}

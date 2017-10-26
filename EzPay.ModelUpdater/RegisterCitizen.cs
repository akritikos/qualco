using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.ModelUpdater
{
    using EzPay.Model.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    class RegisterCitizen
    {
        private readonly UserManager<Citizen> _userManager;
        private readonly SignInManager<Citizen> _signInManager;

        public RegisterCitizen(UserManager<Citizen> userManager, SignInManager<Citizen> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async void Register(Citizen ctz)
        {

            string password = GenerateRandomPassword();

            var result = await _userManager.CreateAsync(ctz, password);
            if (result.Succeeded)
            {
               // _logger.LogInformation("User created a new account with password.");
 
                var emailSender = new EmailSender();
                emailSender.SendPassword("serbeti@hotmail.com", password);
       
                await _signInManager.SignInAsync(ctz, isPersistent: false);
             //   _logger.LogInformation("User created a new account with password.");
                   
            }
            return;

        }

        private string GenerateRandomPassword()
        {
            int length = 8;
            //int numberOfNonAlphanumericCharacters = 2;
            //return GeneratePassword(length, numberOfNonAlphanumericCharacters);

            const string Valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*.";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(Valid[rnd.Next(Valid.Length)]);
            }
            return res.ToString();
        }
    }
}

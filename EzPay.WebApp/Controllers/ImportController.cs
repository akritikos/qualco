using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EzPay.WebApp.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection.PortableExecutable;

    using EzPay.IO;
    using EzPay.Model;
    using EzPay.Model.Entities;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;

    public class ImportController : Controller
    {
        private readonly IEzPayRepository _ctx;

        /// <inheritdoc />
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public ImportController(IEzPayRepository ctx) => _ctx = ctx;

        //[HttpPost]
        //public IActionResult Import()
        //{
        //    var s = $"DEBTS_{DateTime.Now:yyyyMMdd}.txt";
        //    return ImportData(s);
        //}

        /// <summary>
        /// Handles Importing data
        /// (to be called daily, @ 04:00
        /// </summary>
        public async Task<IActionResult> ImportData(string filename)
        {
            var import = new Importer();
            var data = import.GetResults();

            var existingCitizens = _ctx.GetSet<Citizen>().ToDictionary(c => c.Id, c => c);
            var importCitizens = data.ToDictionary(c => c.Key.Id, c => c.Key)
                .Except(existingCitizens)
                .ToDictionary(c => c.Key, c => c.Value);

            var process = 0;

            var toRegister = new List<Citizen>();
            var toMail = new List<Citizen>();

            foreach (var c in data.Keys)
            {
                if (importCitizens.ContainsKey(c.Id))
                {
                    c.ConcurrencyStamp = Guid.NewGuid().ToString();
                    c.UserName = c.Id.ToString();
                    c.NormalizedUserName = c.UserName;
                    c.NormalizedEmail = c.Email;
                    c.EmailConfirmed = true;
                    _ctx.Add(c);
                    toRegister.Add(c);
                }
                process++;
                if (process > 1000)
                {
                    _ctx.SaveChanges();
                    foreach (var citizen in toRegister)
                    {
                            toMail.Add(citizen);
                    }
                    toRegister.Clear();
                    process = 0;
                }
            }
            _ctx.SaveChanges();
            process = 0;
            foreach (var value in data.Values)
            {
                _ctx.AddRange(value);
            }

            _ctx.SaveChanges();
            await _ctx.SaveChangesAsync();
            return new EmptyResult();
        }

    }
}

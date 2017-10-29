using EzPay.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzPay.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    public class CitizenDetailsController : Controller
    {
        private readonly EzPayContext _context;

        public CitizenDetailsController(EzPayContext context)
        {
            _context = context;
        }

        // GET: Citizen/Details/5
        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            var model = await _context.Citizens.Where(c => c.Id == id)
                .FirstOrDefaultAsync();


            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}

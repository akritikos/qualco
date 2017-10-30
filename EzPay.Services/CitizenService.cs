using System;
using System.Collections.Generic;
using System.Text;
using EzPay.Model.Entities;
using EzPay.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EzPay.Services
{
    public class CitizenService : ICitizen
    {
        private EzPayContext _ctx;

        public CitizenService(EzPayContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Citizen> GetAll()
        {
            return _ctx.Citizens;
        }

        public void Add(Citizen newCitizen)
        {
            _ctx.Add(newCitizen);
            _ctx.SaveChanges();
        }

        public Citizen GetById(long id)
        {
            return _ctx.Citizens
                 .Include(ctz => ctz.Bills)
                 .Include(ctz => ctz.Settlements)
                 .FirstOrDefault(ctz => ctz.Id == id);
        }

        public string GetFirstName(long id)
        {
            if (_ctx.Citizens.Any(ctz => ctz.Id == id))
            {
                return _ctx.Citizens.FirstOrDefault(ctz => ctz.Id == id).FirstName;
            }
            else
            {
                return "";
            }
        }

        public string GetLastName(long id)
        {
            if (_ctx.Citizens.Any(ctz => ctz.Id == id))
            {
                return _ctx.Citizens.FirstOrDefault(ctz => ctz.Id == id).LastName;
            }
            else
            {
                return "";
            }
        }

        public Bill GetBills(long id)
        {
            return (Bill)GetById(id).Bills;
        }
    }
}

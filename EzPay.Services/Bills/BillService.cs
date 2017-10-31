using System;
using System.Collections.Generic;
using System.Text;
using EzPay.Model.Entities;
using EzPay.Model;
using System.Linq;

namespace EzPay.Services.Bills
{
    public class BillService : IBill
    {
        public IEnumerable<Bill> GetByCitizenId(long id)
        {
            using (var ctx = new EzPayContext())
            {
                return ctx.Bills.Where(c => c.CitizenId == id);
            }
        }

        public IEnumerable<Bill> GetBySettlementId(Guid id)
        {
            using (var ctx = new EzPayContext())
            {
                return ctx.Bills.Where(c => c.SettlementId == id);
            }
        }

        public decimal GetTotalAmountByCitizen(long id)
        {
            using (var ctx = new EzPayContext())
            {
                return ctx.Bills.Where(c => c.CitizenId == id).Sum(c => c.Amount);
            }
        }
    }
}

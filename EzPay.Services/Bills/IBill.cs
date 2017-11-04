using EzPay.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Services.Bills
{
    public interface IBill
    {
        IEnumerable<Bill> GetByCitizenId(long id);
        IEnumerable<Bill> GetBySettlementId(Guid id);
        decimal GetTotalAmountByCitizen(long id);
    }
}

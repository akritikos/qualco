using EzPay.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzPay.WebApp.Models
{
    public class BillViewModel
    {
        public IEnumerable<Bill> Bills { get; set; }
    }
}

using EzPay.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzPay.WebApp.Models
{
    public class BillViewModel
    {
        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        //public virtual Citizen CitizenId { get; set; }

        //public virtual Settlement Settlements { get; set; }
    }
}

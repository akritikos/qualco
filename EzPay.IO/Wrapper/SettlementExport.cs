using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.IO.Wrapper
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Settlement"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Required formatting for export, avoiding data annotation.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Properties are private & documented in Payment class")]
    [DelimitedRecord(";")]
    public class SettlementExport
    {
        
        public SettlementExport(Settlement p)
        {
            var sb = new StringBuilder();
            VAT = p.Bills.First().Citizen.Id.ToString();
            TIME = p.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            foreach (var bill in p.Bills)
            {
                sb.Append(bill.Id.ToString());
            }

            BILLS = sb.ToString();
            DOWNPAYMENT = p.Type.Downpayment.ToString(CultureInfo.CreateSpecificCulture("en-US"));
            INSTALLMENTS = p.Installments.ToString();
            INTEREST = p.Type.Interest.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }

        public SettlementExport()
        {
            
        }

        public string VAT { get; set; }

        public string TIME { get; set; }

        public string BILLS { get; set; }

        public string DOWNPAYMENT { get; set; }

        public string INSTALLMENTS { get; set; }

        public string INTEREST { get; set; }
    }
}

﻿using System.Text;

namespace EzPay.IO.ExportWrappers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Settlement"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    [DelimitedRecord(";")]
    public class SettlementExport : IEntityRecord
    {
        private long vat;

        private string time;

        private string bills;

        [FieldConverter(ConverterKind.Decimal, ",")]
        private decimal downpayment;

        private int installments;

        [FieldConverter(ConverterKind.Decimal, ",")]
        private decimal interest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettlementExport"/> class.
        /// </summary>
        /// <param name="s">The <see cref="Settlement"/> to export</param>
        public SettlementExport(Settlement s)
        {
            var sb = new StringBuilder();
            vat = s.Bills.First().Citizen.Id;
            time = $"{s.Date.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}";
            foreach (var bill in s.Bills)
            {
                sb.Append($"{{{bill.Id}}}");
            }

            bills = sb.ToString();
            downpayment = s.Type.Downpayment;
            installments = s.Installments;
            interest = s.Type.Interest;
        }

        public SettlementExport()
        {
        }
    }
}
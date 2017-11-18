using System.Text;

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
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "FileHelpers needs to be able to access fields")]
    [DelimitedRecord(";")]
    public class SettlementExport : IEntityRecord
    {
        public long Vat;

        public string Time;

        public string Bills;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Downpayment;

        public int Installments;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Interest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettlementExport"/> class.
        /// </summary>
        /// <param name="s">The <see cref="Settlement"/> to export</param>
        public SettlementExport(Settlement s)
        {
            var sb = new StringBuilder();
            Vat = s.Bills.First().Citizen.Id;
            Time = $"{s.Date.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}";
            foreach (var bill in s.Bills)
            {
                sb.Append($"{{{bill.Id}}}");
            }

            Bills = sb.ToString();
            Downpayment = s.Type.Downpayment;
            Installments = s.Installments;
            Interest = s.Type.Interest;
        }

        public SettlementExport()
        {
        }
    }
}

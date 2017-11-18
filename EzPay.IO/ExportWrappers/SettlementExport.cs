namespace EzPay.IO.ExportWrapper
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Settlement"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Required formatting for export, avoiding data annotation.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in corresponding classes")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [DelimitedRecord(";")]
    public class SettlementExport : IEntityRecord
    {
        public string VAT;

        public string TIME;

        public string BILLS;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal DOWNPAYMENT;

        public int INSTALLMENTS;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal INTEREST;

        public SettlementExport()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettlementExport"/> class.
        /// </summary>
        /// <param name="s">
        /// The <see cref="Settlement"/> to export
        /// </param>
        public SettlementExport(Settlement s)
        {
            var sb = new StringBuilder();
            this.VAT = s.Bills.First().Citizen.Id.ToString();
            this.TIME = s.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            foreach (var bill in s.Bills)
            {
                sb.Append($"{{{bill.Id}}}");
            }

            this.BILLS = sb.ToString();
            this.DOWNPAYMENT = s.Type.Downpayment;
            this.INSTALLMENTS = s.Installments;
            this.INTEREST = s.Type.Interest;
        }
    }
}

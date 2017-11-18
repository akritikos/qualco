namespace EzPay.IO.ExportWrappers
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Payment"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Fields are documented in respective classes")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [DelimitedRecord(";")]
    public class PaymentExport : IEntityRecord
    {
        public string Bill;

        public string Time;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Amount;

        public string Method;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentExport"/> class.
        /// </summary>
        /// <param name="p">
        /// The <see cref="Payment"/> to export
        /// </param>
        public PaymentExport(Payment p)
        {
            Bill = p.BillId.ToString();
            Time = $"{p.Date.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}";
            Amount = p.Bill.Amount;
            Method = p.Method;
        }

        public PaymentExport()
        {
        }
    }
}

namespace EzPay.IO.ExportWrapper
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.IO.Interfaces;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Payment"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Required formatting for export, avoiding data annotation.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Properties are private & documented in Payment class")]
    [SuppressMessage("ReSharper", "StyleCop.SA1401", Justification = "Entity is meant for record parsing, access modifier is Public for FileHelpers interop on fields")]
    [DelimitedRecord(";")]
    public class PaymentExport : IEntityRecord
    {
        public string BILL_ID;

        public string TIME;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal AMOUNT;

        public string METHOD;

        public PaymentExport()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentExport"/> class.
        /// </summary>
        /// <param name="p">
        /// The <see cref="Payment"/> to export
        /// </param>
        public PaymentExport(Payment p)
        {
            this.BILL_ID = p.BillId.ToString();
            this.TIME = $"{p.Date.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}";
            this.AMOUNT = p.Bill.Amount;
            this.METHOD = p.Method;
        }
    }
}

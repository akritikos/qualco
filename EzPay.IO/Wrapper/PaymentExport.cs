namespace EzPay.IO.Wrapper
{
    using System.Diagnostics.CodeAnalysis;

    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// Wrapper class for exporting CSV records of <see cref="Payment"/> entities
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Required formatting for export, avoiding data annotation.")]
    [SuppressMessage("ReSharper", "StyleCop.SA1600", Justification = "Properties are private & documented in Payment class")]
    [DelimitedRecord(";")]
    public class PaymentExport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentExport"/> class.
        /// </summary>
        /// <param name="p">
        /// The <see cref="Payment"/> to export
        /// </param>
        public PaymentExport(Payment p)
        {
            BILL_ID = p.BillId.ToString();
            TIME = p.Date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            AMOUNT = p.Bill.Amount;
            METHOD = p.Method;
        }

        public PaymentExport()
        {
            
        }

        public string BILL_ID { get; }

        public string TIME { get; }

        public decimal AMOUNT { get; }

        public string METHOD { get; }
    }
}

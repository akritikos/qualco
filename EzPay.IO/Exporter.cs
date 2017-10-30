using System.Collections.Generic;

namespace EzPay.IO
{
    using System.IO;
    using System.Linq;

    using EzPay.IO.Wrapper;
    using EzPay.Model;
    using EzPay.Model.Entities;

    using FileHelpers;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Exporter handing <see cref="Payment"/> and <see cref="Settlement"/> entities
    /// </summary>
    public class Exporter
    {
        /// <summary>
        /// Header for Settlement export file
        /// </summary>
        private const string SettlementHeader = "VAT; TIME;BILLS;DOWNPAYMENT;INSTALLMENTS;INTEREST";

        /// <summary>
        /// Header for Payment export file
        /// </summary>
        private const string PaymentHeader = "BILL_ID;TIME;AMOUNT;METHOD";

        /// <summary>
        /// FileHelperEngine managing <see cref="Payment"/> records
        /// </summary>
        private readonly FileHelperEngine<PaymentExport> paymentEngine;

        /// <summary>
        /// FileHelperEngine managing <see cref="Settlement"/> records
        /// </summary>
        private readonly FileHelperEngine<SettlementExport> settlementEngine;

        /// <summary>
        /// List containing <see cref="Payment"/> records to be exported
        /// </summary>
        private readonly List<PaymentExport> paymentExports;

        /// <summary>
        /// List containing <see cref="Settlement"/> records to be exported
        /// </summary>
        private readonly List<SettlementExport> settlementExports;

        /// <summary>
        /// Initializes a new instance of the <see cref="Exporter"/> class.
        /// </summary>
        public Exporter()
        {
            paymentExports = new List<PaymentExport>();
            settlementExports = new List<SettlementExport>();

            paymentEngine = new FileHelperEngine<PaymentExport>() { HeaderText = PaymentHeader };
            settlementEngine = new FileHelperEngine<SettlementExport>() { HeaderText = SettlementHeader };
            using (var ctx = new EzPayContext())
            {
                var payments = ctx.Payments.Include(c => c.Bill).ToList();
                var settlements = ctx.Settlements
                    .Include(s => s.Bills)
                    .Include(s => s.Type)
                    .Include(s => s.Citizen)
                    .ToList();
                foreach (var payment in payments)
                {
                    paymentExports.Add(new PaymentExport(payment));
                }
                foreach (var settlement in settlements)
                {
                    settlementExports.Add(new SettlementExport(settlement));
                }
            }
        }

        /// <summary>
        /// Writes a list of available <see cref="Payment"/> records
        /// </summary>
        /// <param name="path">The file to write data to</param>
        public void ExportPayments(string path)
        {
            using (var fi = new FileStream(path, FileMode.OpenOrCreate))
            {
                StreamWriter write = new StreamWriter(fi);
                paymentEngine.WriteStream(write, paymentExports);
                write.Flush();
                write.Close();
            }
        }

        public void ExportSettlements(string path)
        {
            using (var fi = new FileStream(path, FileMode.OpenOrCreate))
            {
                var write = new StreamWriter(fi);
                settlementEngine.WriteStream(write, settlementExports);
                write.Flush();
                write.Close();
            }
        }
    }
}

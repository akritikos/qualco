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
        /// FileHelperEngine managing <see cref="Payment"/> records
        /// </summary>
        private readonly FileHelperEngine<PaymentExport> paymentEngine;

        /// <summary>
        /// List containing <see cref="Payment"/> records to be exported
        /// </summary>
        private readonly List<PaymentExport> paymentExports;

        /// <summary>
        /// Initializes a new instance of the <see cref="Exporter"/> class.
        /// </summary>
        public Exporter()
        {
            paymentExports = new List<PaymentExport>();
            paymentEngine = new FileHelperEngine<PaymentExport>();
            using (var ctx = new EzPayContext())
            {
                var payments = ctx.Payments.Include(c => c.Bill).ToList();
                foreach (var payment in payments)
                {
                    paymentExports.Add(new PaymentExport(payment));
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
    }
}

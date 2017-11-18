using System;

namespace EzPay.Export
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using EzPay.IO;
    using EzPay.IO.ExportWrappers;
    using EzPay.Model;
    using EzPay.Model.Entities;
    using EzPay.Services.Utilities;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Console app triggering exports
    /// </summary>
    public class Program
    {
        private static IEzPayRepository _ctx;

        private static readonly DirectoryInfo rootDir = new DirectoryInfo(@"C:\ezpay");

        private static IConfigFile config;

        private static bool _local;

        /// <summary>
        /// Entry point for Console App
        /// </summary>
        /// <param name="args">Optional args</param>
        public static void Main(string[] args)
        {
            _local = false;
            config.LoadConfig(new FileInfo(Path.Combine(rootDir.FullName, "priv", "appsettings.txt")));
            var exportRoot = new DirectoryInfo(Path.Combine(rootDir.FullName, "export"));
            if (!exportRoot.Exists)
            {
                Console.WriteLine("Export share is not available!");
            }

            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(_local ? config.GetConfigValue("SQLExpress") : config.GetConfigValue("SQL"));
            _ctx = new EzPaySqlServerContext(options.Options);

            var settleExports = _ctx.GetSet<Settlement>().Include(b => b.Bills)
                .Include(s => s.Type).Select(c => new SettlementExport(c));
            var payExports = _ctx.GetSet<Payment>().Select(p => new PaymentExport(p));
            var date = $"{DateTime.Now.AddDays(-1):yyyyMMdd}";

            Exporter.ExportRecords(
                settleExports,
                Path.Combine(exportRoot.FullName, $"SETTLEMENTS_{date}.txt"),
                "VAT;TIME;BILLS;DOWNPAYMENT;INSTALLMENTS;INTEREST");
            Exporter.ExportRecords(
                payExports,
                Path.Combine(exportRoot.FullName, $"PAYMENTS_{date}.txt"),
                "BILL_ID;TIME;AMOUNT;METHOD");
        }
    }
}

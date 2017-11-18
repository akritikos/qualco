using System;

namespace EzPay.Import
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;

    using EzPay.IO;
    using EzPay.IO.ImportWrappers;
    using EzPay.Model;
    using EzPay.Model.Entities;
    using EzPay.Services;
    using EzPay.Services.Utilities;

    using FileHelpers;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.SqlServer.Dts.Runtime;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Entry point for Console App")]
    public sealed class Program
    {
        public static DirectoryInfo rootDir = new DirectoryInfo(@"C:\ezpay");

        private static bool _local;

        private static IEzPayRepository ctx;

        private static IConfigFile config = new ConfigFile();

        private static ErrorInfo[] errors;

        /// <summary>
        /// Entry point for console app
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            _local = true;
            if (!rootDir.Exists || !rootDir.GetDirectories()
                    .Any(r => r.FullName.Equals(Path.Combine(rootDir.FullName, "import"))))
            {
                return;
            }

            var file = new FileInfo(
                Path.Combine(rootDir.FullName, "import", $"DEBTS_{DateTime.Now:yyyMMdd}.txt"));
#if(INITIAL)
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(rootDir.FullName, "import", "CitizenDebts_1M_3.txt"));
            }
#endif
            if (!file.Exists)
            {
                return;
            }
            config.LoadConfig(new FileInfo(Path.Combine(rootDir.FullName, "priv", "appsettings.txt")));
            var options = new DbContextOptionsBuilder();
            options.UseSqlServer(_local ? config.GetConfigValue("SQLExpress") : config.GetConfigValue("Azure"));
            ctx = new EzPaySqlServerContext(options.Options);
            var filepath = Path.Combine(
                rootDir.FullName,
                "priv",
                _local ? "Local_Import.dtsx" : "Azure_Import.dtsx");
            SplitDebtFiles(file);
            PrintImportErrors();

            RunDtsx(new FileInfo(filepath), "Importing daily records");
            filepath = Path.Combine(
                rootDir.FullName,
                "priv",
                _local ? "Local_Import.dtsx" : "Azure_Import.dtsx");

            // RunDtsx(new FileInfo(filepath), "Import passwords for new users");
            var register = new RegisterCitizens(ctx, _local);
            
        }

        /// <summary>
        /// Runs a DTSX package
        /// </summary>
        /// <param name="file">The file to run</param>
        /// <param name="reason">What running accomplishes</param>
        public static void RunDtsx(FileInfo file, string reason)
        {
            if (!file.Exists)
            {
                Console.WriteLine("SSIS Package not found!");
                return;
            }

            var app = new Application();
            var pkg = app.LoadPackage(file.FullName, null);
            var pkgResults = pkg.Execute();
            Console.WriteLine($"{reason}\n{pkgResults.ToString()}");
        }

        /// <summary>
        /// Splits a file from CitizenDebt format to two flat files in Citizen and Bill format
        /// </summary>
        /// <param name="file">
        /// File containing CitizenDebt data
        /// </param>
        private static void SplitDebtFiles(FileInfo file)
        {
            var import = new Importer(file);
            var data = import.GetResults();
            var citizens = data.Keys.Select(
                citizen => new CitizenRecord()
                {
                    Id = citizen.Id,
                    FirstName = citizen.FirstName,
                    LastName = citizen.LastName,
                    Email = citizen.Email,
                    Phone = citizen.PhoneNumber,
                    Address = citizen.Address,
                    County = citizen.County,
                    Username = citizen.Id.ToString()
                });
            var bills = data.Values.SelectMany(x => x).Select(
                bill => new BillRecord()
                {
                    Id = $"{{{bill.Id}}}",
                    Amount = bill.Amount,
                    Citizen = bill.CitizenId,
                    Description = bill.Description,
                    DueDate = $"{bill.DueDate:yyyy-MM-dd}"
                });
            errors = import.GetErrors();
            Exporter.ExportRecords(
                citizens,
                Path.Combine(file.Directory.FullName, "CITIZENS.CSV"),
                "VAT;FIRST_NAME;LAST_NAME;EMAIL;PHONE;ADDRESS;COUNTY;USERNAME;ACCESS_FAILED_COUNT;EMAIL_CONFIRMED;LOCKOUT_ENABLED;PHONE_CONFIRMED;TWOFACTOR_ENABLED");
            Exporter.ExportRecords(
                bills,
                Path.Combine(file.Directory.FullName, "BILLS.CSV"),
                "ID;AMOUNT;VAT;DESCRIPTION;DUE_DATE");
           import.Dispose();
        }

        private static void PrintImportErrors()
        {
            foreach (var error in errors)
            {
                Console.WriteLine($"\nError on line {error.LineNumber}:");
                Console.WriteLine($"\t{error.ExceptionInfo}");
                Console.WriteLine("Offending record was:");
                Console.WriteLine($"{error.RecordString}");
            }
        }
    }
}

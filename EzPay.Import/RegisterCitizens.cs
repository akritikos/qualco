using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Import
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using EzPay.Import.Internal;
    using EzPay.IO;
    using EzPay.IO.ImportWrappers;
    using EzPay.Model;
    using EzPay.Model.Entities;

    internal class RegisterCitizens
    {

        private readonly IEzPayRepository _ctx;

        /// <summary>
        /// List of Citizens that need to be emailed a temporary password
        /// </summary>
        internal Dictionary<UserContact, string> toNotify { get; }

        private static readonly string passwordChars = "abcdefghijklmnopqrstuvwxyz012345678";

        private static readonly Random rnd = new Random();

        public RegisterCitizens(IEzPayRepository ctx, bool local)
        {
            _ctx = ctx;
            toNotify = new Dictionary<UserContact, string>();
            ExportHashes();
        }

        private void ExportHashes()
        {
            Console.WriteLine("Beginning registration of new _citizens");
            var records = new List<RegistrationRecord>();
            var citizens = this._ctx.GetSet<Citizen>().Where(c => c.PasswordHash == null);
            var current = 1;
            var total = citizens.Count();
            var watch = Stopwatch.StartNew();
            foreach (var citizen in citizens)
            {
                var progress = 100 * current / total;
                var eta = watch.ElapsedTicks * (total - current) / current;
                Console.Write($"\rGenerated passwords for {current - 1} citizens, currently at {progress}%... ETA: {new TimeSpan(eta):mm\\:ss}");
                string plaintext = null;
#if(!INITIAL)
                plaintext = RandomPassword(6);
#endif
                records.Add(
                    new RegistrationRecord()
                    {
                        Vat = citizen.Id,
                        NormalizedEmail = citizen.Email.ToUpper(),
                        NormalizedUsername = citizen.Id.ToString().ToUpper(),
                        PasswordHash =
                                plaintext
                                ?? "AQAAAAEAACcQAAAAECwnSDOZ2Rz1RYgHA8fem+uDkPIzWGYE5TWDWrOUa4VmV8KOLZxIZr3+rRsM7CocCA==",
                        SecurityStamp = $"{{{Guid.NewGuid()}}}",
                        ConcurencyStamp = $"{{{Guid.NewGuid()}}}"
                    });
                toNotify.Add(
                    new UserContact()
                    {
                        Email = citizen.Email,
                        FirstName = citizen.FirstName,
                        LastName = citizen.LastName
                    },
                    plaintext ?? "123");
                current++;
            }
            Exporter.ExportRecords(
                records,
                Path.Combine(Program.rootDir.FullName, "import", "HASHES.CSV"),
                "VAT;NORMALMAIL;NORMALUSER;HASH;CONCURENCY;SECURITY");
        }

        /// <summary>
        /// Creates a one time password to be used after registering
        /// </summary>
        /// <param name="length">Number of characters in resulting password</param>
        /// <returns>Temporary password</returns>
        private static string RandomPassword(int length)
        {
            if (length < 1)
            {
                return String.Empty;
            }
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                result.Append(passwordChars[rnd.Next(passwordChars.Length)]);
            }
            return result.ToString();
        }
    }
}

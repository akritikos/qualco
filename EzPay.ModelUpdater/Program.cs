﻿using System;

namespace EzPay.ModelUpdater
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;

    using EzPay.Model;
    using EzPay.Model.Entities;
    using EzPay.IO;
    using EzPay.Model.Comparer;

    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Dummy Program to allow DB migrations
    /// </summary>
    public static class Program
    {
        private static Stopwatch w;

        /// <summary>
        /// Entry point for Console App
        /// </summary>
        /// <param name="args">Optional arguments</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter password to hash:");
            var plaintext = Console.ReadLine();
            var hash = Hasher(plaintext ?? String.Empty);
            Console.WriteLine($"Hash is: {hash}");
            Console.WriteLine($"Size: {hash.Length}");
            using (var ctx = new EzPayContext())
            {
                w = Stopwatch.StartNew();
                Console.WriteLine("Testing Database and migrating if needed.");
                if (!CheckContext(ctx))
                {
                    return;
                }
                w.Stop();
                Console.WriteLine($"\tDatabase is operational, test took: {w.Elapsed:mm\\:ss\\.ff}");
                AddSettlementTypes(ctx);
                // ImportData();
                ExportData();
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }               
        }

        public static void ExportData()
        {
            var ex = new Exporter();
            ex.ExportPayments(@"D:\Pay.csv");
            ex.ExportSettlements(@"D:\Settle.csv");
        }

        /// <summary>
        /// Adds default Settlement types to a newly created database
        /// </summary>
        /// <param name="ctx">Database Context to use</param>
        private static void AddSettlementTypes(EzPayContext ctx)
        {
            if (ctx.GetSet<SettlementType>().Any())
            {
                return;
            }

            var types = new List<SettlementType>()
                            {
                                new SettlementType()
                                    {
                                        Downpayment = 10,
                                        MaxInstallments = 24,
                                        Interest = new decimal(4.1)
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 20,
                                        MaxInstallments = 24,
                                        Interest = new decimal(3.9)
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 30,
                                        MaxInstallments = 36,
                                        Interest = new decimal(3.6)
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 40,
                                        MaxInstallments = 36,
                                        Interest = new decimal(3.2)
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 50,
                                        MaxInstallments = 48,
                                        Interest = new decimal(2.6)
                                    }
                            };
            ctx.GetSet<SettlementType>().AddRange(types);
            ctx.SaveChanges();
        }

        /// <summary>
        /// Checks if database connection works as expected
        /// </summary>
        /// <param name="ctx">The DbContext to check</param>
        /// <returns>If the Context is working as it should</returns>
        private static bool CheckContext(EzPayContext ctx)
        {
            var isValid = false;
            try
            {
                ctx.Database.Migrate();
                isValid = true;
            }
            catch (SqlException ex)
            {
                if (ex.ErrorCode == -2146232060)
                {
                    try
                    {
                        ctx.Database.EnsureDeleted();
                        ctx.Database.Migrate();
                        isValid = true;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Imports data to database
        /// </summary>
        private static void ImportData()
        {
            var importCitizens = new Dictionary<long, Citizen>();
            var import = new Importer();
            Console.WriteLine("Beginning data import!");
            w = Stopwatch.StartNew();
            var data = import.GetResults();
            w.Stop();
            Console.WriteLine(
                $"\tRead {data.Count:N0} Citizens and {data.Values.SelectMany(x => x).ToList().Count:N0} Bills\n\tin {w.Elapsed:mm\\:ss\\.ff}");
            using (var ctx = new EzPayContext())
            {
                Console.WriteLine("Filtering out duplicate citizens");
                w = Stopwatch.StartNew();
                var currentCitizens = ctx.GetSet<Citizen>().ToDictionary(c => c.Id, c => c);
                importCitizens = data.ToDictionary(c => c.Key.Id, c => c.Key).Except(currentCitizens)
                    .ToDictionary(c => c.Key, c => c.Value);
                w.Stop();
                Console.WriteLine($"\tDone in {w.Elapsed:mm\\:ss\\.ff}");

                // Bills, settlements and payments should be empty!
                ctx.Database.ExecuteSqlCommand("DELETE FROM Payments");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Bills");
                ctx.Database.ExecuteSqlCommand("DELETE FROM Settlements");
            }


           var ctb = new EzPayContext();
            Console.WriteLine("Writing data to database");
            var size = data.Keys.Count;
            w = Stopwatch.StartNew();
            var processed = 0;
            var times = 0;
            foreach (var debt in data)
            {
                if (processed >= 100)
                {
                    processed = 0;
                    times++;
                    ctb.SaveChanges();
                    ctb.Dispose();
                    ctb = new EzPayContext();
                    Console.WriteLine($"\t\tCurrently at {times*1000/size:N0}%");
                }

                if (importCitizens.ContainsKey(debt.Key.Id))
                {
                    debt.Key.UserName = debt.Key.Email;
                    //RegisterCitizen registerCitizen =new RegisterCitizen();
                    //registerCitizen.Register(debt.Key);

                    ctb.Add(debt.Key);
                }

                ctb.AddRange(debt.Value);
                processed++;
            }

            ctb.SaveChanges();
            ctb.Dispose();
            w.Stop();
            Console.WriteLine($"\tElapsed time: {w.Elapsed:mm\\:ss\\.ff}");
            import.Dispose();
        }

        public static string Hasher(string plaintext)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            return Convert.ToBase64String(HashPasswordV3(plaintext, rng, KeyDerivationPrf.HMACSHA256, 10000, 128 / 8, 256 / 8));
        }

        private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return outputBytes;
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }
    }
}

using System;

namespace EzPay.ModelUpdater
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using EzPay.Model;
    using EzPay.Model.Entities;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Dummy Program to allow DB migrations
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry point for Console App
        /// </summary>
        /// <param name="args">Optional arguments</param>
        public static void Main(string[] args)
        {
            using (var ctx = new EzPayContext())
            {
                try
                {
                    ctx.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    // Handles existing database errors
                    if (ex.ErrorCode == -2146232060)
                    {
                        ctx.Database.EnsureDeleted();
                        ctx.Database.Migrate();
                    }
                }
                AddSettlementTypes(ctx);
            }               
        }

        /// <summary>
        /// Adds default Settlement types to a newly created database
        /// </summary>
        /// <param name="ctx">Database Context to use</param>
        private static void AddSettlementTypes(EzPayContext ctx)
        {
            if (ctx.SettlementTypes.Any())
            {
                return;
            }
            var types = new List<SettlementType>()
                            {
                                new SettlementType()
                                    {
                                        Downpayment = 10,
                                        MaxInstallments = 24,
                                        Interest = 4.1
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 20,
                                        MaxInstallments = 24,
                                        Interest = 3.9
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 30,
                                        MaxInstallments = 36,
                                        Interest = 3.6
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 40,
                                        MaxInstallments = 36,
                                        Interest = 3.2
                                    },
                                new SettlementType()
                                    {
                                        Downpayment = 50,
                                        MaxInstallments = 48,
                                        Interest = 2.6
                                    }
                            };
            ctx.SettlementTypes.AddRange(types);
            ctx.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.IO
{
    using System.IO;

    using EzPay.IO.Wrapper;
    using EzPay.Model.Comparer;
    using EzPay.Model.Entities;

    using FileHelpers;

    /// <summary>
    /// A sample class to import data
    /// </summary>
    public class Importer : IDisposable
    {
        /// <summary>
        /// FileHelper engine used to parse liens
        /// </summary>
        private readonly FileHelperAsyncEngine<DebtRecord> engine;

        /// <summary>
        /// Location of file to be imported
        /// </summary>
        private readonly string file;

        /// <summary>
        /// Dictionary to be returned
        /// </summary>
        private Dictionary<Citizen, List<Bill>> data;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Importer"/> class.
        /// </summary>
        /// <param name="filepath">
        /// The complete filepath of the CSV to be imported
        /// </param>
        public Importer(string filepath = @"C:\CitizenDebts_1M_3.csv")
        {
            data = data ?? new Dictionary<Citizen, List<Bill>>();
            engine = new FileHelperAsyncEngine<DebtRecord>();
            file = filepath;
        }

        /// <summary>
        /// Parses data from the file that has been supplied
        /// </summary>
        /// <returns>A dictionary with a Citizen index and a List of their Bills</returns>
        public Dictionary<Citizen, List<Bill>> GetResults()
        {
            if (data.Count == 0)
            {
                using (engine.BeginReadFile(file))
                {
                    foreach (var debt in engine)
                    {
                        var c = debt.ParseCitizen();
                        var b = debt.ParseBill();
                        if (!data.ContainsKey(c))
                        {
                            data.Add(c,new List<Bill>());
                        }
                        data[c].Add(b);
                    }
                }
            }

            return data;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ((IDisposable)engine)?.Dispose();
            data.Clear();
        }
    }
}

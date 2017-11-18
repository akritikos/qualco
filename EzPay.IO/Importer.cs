using System;
using System.Collections.Generic;

namespace EzPay.IO
{
    using System.IO;

    using EzPay.IO.ImportWrappers;
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
        private readonly FileInfo file;

        /// <summary>
        /// Dictionary to be returned
        /// </summary>
        private readonly Dictionary<Citizen, List<Bill>> data;

        /// <summary>
        /// Initializes a new instance of the <see cref="Importer"/> class.
        /// </summary>
        /// <param name="importFile">
        /// The complete filepath of the CSV to be imported
        /// </param>
        public Importer(FileInfo importFile)
        {
            data = data ?? new Dictionary<Citizen, List<Bill>>();
            engine = new FileHelperAsyncEngine<DebtRecord>();
            file = importFile;
        }

        /// <summary>
        /// Parses data from the file that has been supplied
        /// </summary>
        /// <returns>A dictionary with a Citizen index and a List of their Bills</returns>
        public Dictionary<Citizen, List<Bill>> GetResults()
        {
            if (data.Count != 0)
            {
                return data;
            }

            engine.ErrorMode = ErrorMode.SaveAndContinue;
            using (engine.BeginReadFile(file.FullName))
            {
                foreach (var debt in engine)
                {
                    var c = debt.ParseCitizen();
                    var b = debt.ParseBill();
                    if (!data.ContainsKey(c))
                    {
                        data.Add(c, new List<Bill>());
                    }

                    data[c].Add(b);
                }
            }

            PrintErrors();
            return data;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            ((IDisposable)engine)?.Dispose();
            data.Clear();
        }

        /// <summary>
        /// Gets errors that occured while parsing
        /// </summary>
        /// <returns><see cref="ErrorInfo"/> array</returns>
        public ErrorInfo[] GetErrors() 
            => engine.ErrorManager.HasErrors ? 
                engine.ErrorManager.Errors 
                : new ErrorInfo[0];

        /// <summary>
        /// Outputs errors to console
        /// </summary>
        private void PrintErrors()
        {
            foreach (var error in engine.ErrorManager.Errors)
            {
                Console.WriteLine();
                Console.WriteLine($"Error on Line number: {error.LineNumber}");
                Console.WriteLine($"\tErroneous record: {error.RecordString}");
                Console.WriteLine($"\tError Info: {error.ExceptionInfo}");
            }
        }
    }
}

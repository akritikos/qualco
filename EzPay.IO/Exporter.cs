using System.Collections.Generic;

namespace EzPay.IO
{
    using System.IO;
    using EzPay.IO.Interfaces;
    using FileHelpers;
    
    /// <summary>
    /// Exporter handing generic EzPay entities
    /// </summary>
    public static class Exporter
    {
        /// <summary>
        /// Saves an exportable IEnumerable to a flat file in the path provided
        /// </summary>
        /// <typeparam name="T">Type being saved</typeparam>
        /// <param name="entities">The IEnumerable to save</param>
        /// <param name="filepath">Where to save the file</param>
        /// <param name="headertext">Optional header for the resulting file</param>
        public static void ExportRecords<T>(IEnumerable<T> entities, string filepath, string headertext = null)
            where T : class, IEntityRecord
        {
            var engine = new FileHelperEngine<T>() { HeaderText = headertext ?? string.Empty };
            var f = new FileInfo(filepath);
            if (f.Exists)
            {
                f.Delete();
            }

            using (var fi = new FileStream(filepath, FileMode.CreateNew))
            {
                var write = new StreamWriter(fi);
                engine.WriteStream(write, entities);
                write.Flush();
                write.Close();
            }
        }
    }
}

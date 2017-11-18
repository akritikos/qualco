using System;
using System.Collections.Generic;
using System.Text;

namespace EzPay.Services
{
    using System.IO;

    using EzPay.Services.Utilities;

    /// <summary>
    /// Flat file storage (tab seperated {key,value} pairs
    /// </summary>
    public class ConfigFile : IConfigFile
    {
        private readonly Dictionary<string, string> config = new Dictionary<string, string>();
        
        /// <summary>
        /// Tries to retrieve a configuration setting from file storage
        /// </summary>
        /// <param name="key">Name of the setting to retrieve</param>
        /// <returns>Value of the setting requested</returns>
        public string GetConfigValue(string key) => 
            (key == null || !config.ContainsKey(key)) ? string.Empty : config[key];

        /// <summary>
        /// Loads a Flat File with required settings
        /// </summary>
        /// <param name="file">The file containing desired settings</param>
        public void LoadConfig(FileInfo file)
        {
            if (!file.Exists)
            {
                return;
            }
            string[] lines;
            try
            {
                lines = System.IO.File.ReadAllLines(file.FullName);
            }
            catch (Exception)
            {
                lines = new string[0];
            }
            foreach (var line in lines)
            {
                var e = line.Split('\t');
                if (e.Length != 2)
                {
                    continue;
                }

                config.Add(e[0],e[1]);
            }
        }
    }
}

namespace EzPay.Services.Utilities
{
    using System.IO;

    /// <summary>
    /// Generic file storage for config settings
    /// </summary>
    public interface IConfigFile
    {
        string GetConfigValue(string key);

        void LoadConfig(FileInfo file);
    }
}

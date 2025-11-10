using System;
using partycli.Properties;
using partycli.Services.Interfaces;

namespace partycli.Services
{
    public class StorageService : IStorageService
    {
        public void StoreValue(string name, string value, bool writeToConsole = true)
        {
            try
            {
                var settings = Settings.Default;
                settings[name] = value;
                settings.Save();
                if (writeToConsole) Console.WriteLine($"Changed {name} to {value}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[StoreValue]: Error: Couldn't save {name}. Exception: {ex.Message}");
            }
        }
    }
}
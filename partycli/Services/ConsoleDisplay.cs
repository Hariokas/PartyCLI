using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace partycli.Services
{
    public static class ConsoleDisplay
    {
        public static void DisplayServersInfo(string serverListString)
        {
            var serverList = new List<ServerModel>();
            try
            {
                serverList = JsonConvert.DeserializeObject<List<ServerModel>>(serverListString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse server list. Exception: {ex.Message}");
                return;
            }

            Console.WriteLine("Server list: ");
            foreach (var t in serverList)
            {
                Console.WriteLine($"Name: {t.Name}");
                Console.WriteLine($"  Load: {t.Load}%");
                Console.WriteLine($"  Status: {t.Status}");
                Console.WriteLine(new string('-', Console.WindowWidth >= 20 ? 20 : Console.WindowWidth));
            }

            Console.WriteLine("Total servers: " + serverList.Count);
        }
    }
}
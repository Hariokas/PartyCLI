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

        public static void ShowHelp()
        {
            Console.WriteLine("To get and save all servers, use command: partycli.exe server_list");
            Console.WriteLine("To get and save France servers, use command: partycli.exe server_list --france");
            Console.WriteLine(
                "To get and save servers that support TCP protocol, use command: partycli.exe server_list --TCP");
            Console.WriteLine("To see saved list of servers, use command: partycli.exe server_list --local ");
        }
    }
}
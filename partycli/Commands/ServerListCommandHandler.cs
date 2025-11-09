using System;
using System.Threading.Tasks;
using partycli.Constants;
using partycli.Properties;
using partycli.Services;

namespace partycli.Commands
{
    public static class ServerListCommandHandler
    {
        public static async Task HandleAsync(bool local, bool france, bool tcp)
        {
            if (local)
            {
                HandleLocal();
                return;
            }

            if (france)
            {
                await HandleFranceAsync();
                return;
            }

            if (tcp)
            {
                await HandleTcpAsync();
                return;
            }

            await HandleAllServersAsync();
        }

        private static void HandleLocal()
        {
            if (!string.IsNullOrWhiteSpace(Settings.Default.serverlist))
                ConsoleDisplay.DisplayServersInfo(Settings.Default.serverlist);
            else
                Console.WriteLine("Error: There are no server data in local storage");
        }

        private static async Task HandleFranceAsync()
        {
            var vpnQuery = new VpnServerQuery(protocol: null, countryId: VpnConstants.CountryFrance, cityId: null, regionId: null, specificServerId: null, serverGroupId: null);
            var serverList = await ApiClient.GetAllServerByCountryListAsync(vpnQuery.CountryId.Value);
            if (string.IsNullOrEmpty(serverList)) 
            {
                Console.WriteLine("Error: No servers found for France.");
                return;
            }
            
            SaveAndDisplayServers(serverList);
        }

        private static async Task HandleTcpAsync()
        {
            var vpnQuery = new VpnServerQuery(protocol: VpnConstants.ProtocolTcp, countryId: null, cityId: null, regionId: null, specificServerId: null, serverGroupId: null);
            var serverList = await ApiClient.GetAllServerByProtocolListAsync(vpnQuery.Protocol.Value);
            if (string.IsNullOrEmpty(serverList)) 
            {
                Console.WriteLine("Error: No servers found for TCP protocol.");
                return;
            }
            
            SaveAndDisplayServers(serverList);
        }

        private static async Task HandleAllServersAsync()
        {
            var serverList = await ApiClient.GetAllServersListAsync();
            if (string.IsNullOrEmpty(serverList)) 
            {
                Console.WriteLine("Error: No servers found.");
                return;
            }
            
            SaveAndDisplayServers(serverList);
        }

        private static void SaveAndDisplayServers(string serverList)
        {
            StorageService.StoreValue("serverlist", serverList, false);
            LogService.Log($"Saved new server list: {serverList}");
            ConsoleDisplay.DisplayServersInfo(serverList);
        }
    }
}
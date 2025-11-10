using System;
using System.Threading.Tasks;
using partycli.Constants;
using partycli.Services.Interfaces;

namespace partycli.Commands
{
    public class ServerListCommandHandler
    {
        public ServerListCommandHandler(IApiClient apiClient, IStorageService storageService, ILogService logService,
            IConsoleDisplay consoleDisplay)
        {
            ApiClient = apiClient;
            StorageService = storageService;
            LogService = logService;
            ConsoleDisplay = consoleDisplay;
        }

        private IApiClient ApiClient { get; }
        private IStorageService StorageService { get; }
        private ILogService LogService { get; }
        private IConsoleDisplay ConsoleDisplay { get; }

        public async Task HandleAsync(bool local, bool france, bool tcp)
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

        private void HandleLocal()
        {
            var serverList = StorageService.GetValue("serverlist");
            if (!string.IsNullOrWhiteSpace(serverList))
                ConsoleDisplay.DisplayServersInfo(serverList);
            else
                Console.WriteLine("Error: There are no server data in local storage");
        }

        private async Task HandleFranceAsync()
        {
            var vpnQuery = new VpnServerQuery(null, VpnConstants.CountryFrance, null, null, null, null);
            var serverList = await ApiClient.GetAllServerByCountryListAsync(vpnQuery.CountryId.Value);
            if (string.IsNullOrEmpty(serverList))
            {
                Console.WriteLine("Error: No servers found for France.");
                return;
            }

            SaveAndDisplayServers(serverList);
        }

        private async Task HandleTcpAsync()
        {
            var vpnQuery = new VpnServerQuery(VpnConstants.ProtocolTcp, null, null, null, null, null);
            var serverList = await ApiClient.GetAllServerByProtocolListAsync(vpnQuery.Protocol.Value);
            if (string.IsNullOrEmpty(serverList))
            {
                Console.WriteLine("Error: No servers found for TCP protocol.");
                return;
            }

            SaveAndDisplayServers(serverList);
        }

        private async Task HandleAllServersAsync()
        {
            var serverList = await ApiClient.GetAllServersListAsync();
            if (string.IsNullOrEmpty(serverList))
            {
                Console.WriteLine("Error: No servers found.");
                return;
            }

            SaveAndDisplayServers(serverList);
        }

        private void SaveAndDisplayServers(string serverList)
        {
            StorageService.StoreValue("serverlist", serverList, false);
            LogService.Log($"Saved new server list: {serverList}");
            ConsoleDisplay.DisplayServersInfo(serverList);
        }
    }
}
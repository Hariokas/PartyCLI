using System;
using System.Threading;
using System.Threading.Tasks;
using partycli.Models.Constants;
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

        public async Task HandleAsync(bool local, bool france, bool tcp, CancellationToken ct = default)
        {
            if (local)
            {
                HandleLocal();
                return;
            }

            if (france)
            {
                await HandleCountryAsync(VpnConstants.Country.France, ct);
                return;
            }

            if (tcp)
            {
                await HandleProtocolAsync(VpnConstants.Protocol.Tcp, ct);
                return;
            }

            await HandleAllServersAsync(ct);
        }

        private void HandleLocal()
        {
            var serverList = StorageService.GetValue("serverlist");
            if (!string.IsNullOrWhiteSpace(serverList))
                ConsoleDisplay.DisplayServersInfo(serverList);
            else
                Console.WriteLine("Error: There are no server data in local storage");
        }

        private async Task HandleCountryAsync(VpnConstants.Country country, CancellationToken ct = default)
        {
            await FetchAndDisplayAsync(
                () => ApiClient.GetAllServerByCountryListAsync(country, ct),
                $"Error: No servers found for country '{country}'.",
                ct);
        }

        private async Task HandleProtocolAsync(VpnConstants.Protocol protocol, CancellationToken ct = default)
        {
            await FetchAndDisplayAsync(
                () => ApiClient.GetAllServerByProtocolListAsync(protocol, ct),
                $"Error: No servers found for protocol '{protocol}'.",
                ct);
        }

        private async Task HandleAllServersAsync(CancellationToken ct = default)
        {
            await FetchAndDisplayAsync(
                () => ApiClient.GetAllServersListAsync(ct),
                "Error: No servers found.",
                ct);
        }

        private async Task FetchAndDisplayAsync(Func<Task<string>> fetchFunction, string notFoundMessage,
            CancellationToken ct = default)
        {
            string serverList;
            try
            {
                serverList = await fetchFunction();
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[FetchAndDisplayAsync]: Operation was canceled.");
                return;
            }
            catch (Exception ex)
            {
                LogService.Log($"Fetch error: {ex.Message}");
                Console.WriteLine("[FetchAndDisplayAsync]: An error occurred while fetching server data.");
                return;
            }

            ct.ThrowIfCancellationRequested();
            
            if (string.IsNullOrEmpty(serverList))
            {
                Console.WriteLine(notFoundMessage);
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
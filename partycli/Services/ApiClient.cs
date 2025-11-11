using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using partycli.Services.Interfaces;

namespace partycli.Services
{
    public class ApiClient : IApiClient
    {
        public ApiClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        private HttpClient Client { get; }

        public async Task<string> GetAllServersListAsync(CancellationToken ct = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "v1/servers");
                var response = await Client.SendAsync(request, ct);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllServersListAsync] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetAllServerByCountryListAsync(int countryId, CancellationToken ct = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"v1/servers?filters[servers_technologies][id]=35&filters[country_id]={countryId}");
                var response = await Client.SendAsync(request, ct);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllServerByCountryListAsync] Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<string> GetAllServerByProtocolListAsync(int vpnProtocol, CancellationToken ct = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"v1/servers?filters[servers_technologies][id]={vpnProtocol}");
                var response = await Client.SendAsync(request, ct);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllServerByProtocolListAsync] Exception: {ex.Message}");
                return null;
            }
        }
    }
}
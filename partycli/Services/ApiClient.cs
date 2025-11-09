using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace partycli.Services
{
    public static class ApiClient
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<string> GetAllServersListAsync()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.nordvpn.com/v1/servers");
                var response = await Client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllServersListAsync] Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> GetAllServerByCountryListAsync(int countryId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    "https://api.nordvpn.com/v1/servers?filters[servers_technologies][id]=35&filters[country_id]=" +
                    countryId);
                var response = await Client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[GetAllServerByCountryListAsync] Exception: {ex.Message}");
                return null;
            }
        }

        public static async Task<string> GetAllServerByProtocolListAsync(int vpnProtocol)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    "https://api.nordvpn.com/v1/servers?filters[servers_technologies][id]=" + vpnProtocol);
                var response = await Client.SendAsync(request);
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
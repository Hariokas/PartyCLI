using System.Threading.Tasks;

namespace partycli.Services.Interfaces
{
    public interface IApiClient
    {
        Task<string> GetAllServersListAsync();
        Task<string> GetAllServerByCountryListAsync(int countryId);
        Task<string> GetAllServerByProtocolListAsync(int vpnProtocol);
    }
}
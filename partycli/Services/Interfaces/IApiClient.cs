using System.Threading;
using System.Threading.Tasks;

namespace partycli.Services.Interfaces
{
    public interface IApiClient
    {
        Task<string> GetAllServersListAsync(CancellationToken ct = default);
        Task<string> GetAllServerByCountryListAsync(int countryId, CancellationToken ct = default);
        Task<string> GetAllServerByProtocolListAsync(int vpnProtocol, CancellationToken ct = default);
    }
}
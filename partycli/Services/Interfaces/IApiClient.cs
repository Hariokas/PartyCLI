using System.Threading;
using System.Threading.Tasks;
using partycli.Models.Constants;

namespace partycli.Services.Interfaces
{
    public interface IApiClient
    {
        Task<string> GetAllServersListAsync(CancellationToken ct = default);
        Task<string> GetAllServerByCountryListAsync(VpnConstants.Country country, CancellationToken ct = default);
        Task<string> GetAllServerByProtocolListAsync(VpnConstants.Protocol protocol, CancellationToken ct = default);
    }
}
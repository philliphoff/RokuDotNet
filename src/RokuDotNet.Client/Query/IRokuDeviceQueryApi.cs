using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Query
{
    public interface IRokuDeviceQueryApi
    {
        Task<GetActiveAppResult> GetActiveAppAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<GetAppsResult> GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
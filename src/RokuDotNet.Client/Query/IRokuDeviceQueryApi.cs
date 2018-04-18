using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Query
{
    public interface IRokuDeviceQueryApi
    {
        Task<GetAppsResult> GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Apps
{
    public interface IRokuDeviceApps
    {
        Task<GetActiveAppResult> GetActiveAppAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<GetActiveTvChannelResult> GetActiveTvChannelAsync(CancellationToken cancellationToken = default(CancellationToken));
    
        Task<GetAppsResult> GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<GetTvChannelsResult> GetTvChannelsAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task InstallAppAsync(string appId, CancellationToken cancellationToken = default(CancellationToken));
        Task InstallAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task LaunchAppAsync(string appId, CancellationToken cancellationToken = default(CancellationToken));
        Task LaunchAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task LaunchTvInputAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task LaunchTvInputAsync(string channel, CancellationToken cancellationToken = default(CancellationToken));
    }
}
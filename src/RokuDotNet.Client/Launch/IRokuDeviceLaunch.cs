using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client.Launch
{
    public interface IRokuDeviceLaunch
    {
        Task InstallAppAsync(string appId, CancellationToken cancellationToken = default(CancellationToken));
        Task InstallAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task LaunchAppAsync(string appId, CancellationToken cancellationToken = default(CancellationToken));
        Task LaunchAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken = default(CancellationToken));

        Task LaunchTvInputAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task LaunchTvInputAsync(string channel, CancellationToken cancellationToken = default(CancellationToken));
    }
}
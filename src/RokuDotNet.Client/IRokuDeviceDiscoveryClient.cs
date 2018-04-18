using System;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client
{
    public interface IRokuDeviceDiscoveryClient
    {
        event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered;

        Task DiscoverDevicesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task DiscoverDevicesAsync(Func<IRokuDevice, Task<bool>> onDeviceDiscovered, CancellationToken cancellationToken = default(CancellationToken));
    }
}

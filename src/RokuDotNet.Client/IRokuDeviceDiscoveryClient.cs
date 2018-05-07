using System;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client
{
    public interface IRokuDeviceDiscoveryClient
    {
        event EventHandler<DeviceDiscoveredEventArgs> DeviceDiscovered;

        void DiscoverDevicesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void DiscoverDevicesAsync(Func<DiscoveredDeviceContext, Task<bool>> onDeviceDiscovered, CancellationToken cancellationToken = default(CancellationToken));
    }
}

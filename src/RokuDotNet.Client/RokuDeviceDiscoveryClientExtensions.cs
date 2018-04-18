using System;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client
{
    public static class RokuDeviceDiscoveryClientExtensions
    {
        public static async Task<IRokuDevice> DiscoverFirstDeviceAsync(this IRokuDeviceDiscoveryClient client, CancellationToken cancellationToken = default(CancellationToken))
        {
            IRokuDevice device = null;

            await client.DiscoverDevicesAsync(
                discoveredDevice =>
                {
                    device = discoveredDevice;

                    return Task.FromResult(true);
                });

            return device;
        }

        public static async Task<IRokuDevice> DiscoverSpecificDeviceAsync(this IRokuDeviceDiscoveryClient client, string serialNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            IRokuDevice device = null;

            await client.DiscoverDevicesAsync(
                discoveredDevice =>
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(discoveredDevice.SerialNumber, serialNumber))
                    {
                        device = discoveredDevice;

                        return Task.FromResult(true);
                    }

                    return Task.FromResult(false);
                });

            return device;
        }
    }
}
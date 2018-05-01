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
                context =>
                {
                    device = context.Device;

                    return Task.FromResult(true);
                });

            return device;
        }

        public static async Task<IRokuDevice> DiscoverSpecificDeviceAsync(this IRokuDeviceDiscoveryClient client, string serialNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            IRokuDevice device = null;

            await client.DiscoverDevicesAsync(
                context =>
                {
                    if (StringComparer.OrdinalIgnoreCase.Equals(context.SerialNumber, serialNumber))
                    {
                        device = context.Device;

                        return Task.FromResult(true);
                    }

                    return Task.FromResult(false);
                });

            return device;
        }
    }
}
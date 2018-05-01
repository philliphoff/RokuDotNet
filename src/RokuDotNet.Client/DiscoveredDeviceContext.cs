using System;

namespace RokuDotNet.Client
{
    public sealed class DiscoveredDeviceContext
    {
        public DiscoveredDeviceContext(IRokuDevice device, Uri location, string serialNumber)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
        }

        public IRokuDevice Device { get; }

        public Uri Location { get; }

        public string SerialNumber { get; }
    }
}
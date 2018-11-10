using System;

namespace RokuDotNet.Client
{
    public class DiscoveredDeviceContext
    {
        public DiscoveredDeviceContext(IRokuDevice device, string serialNumber)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
        }

        public IRokuDevice Device { get; }

        public string SerialNumber { get; }
    }
}
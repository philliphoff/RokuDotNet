using System;

namespace RokuDotNet.Client
{
    public sealed class DeviceDiscoveredEventArgs : EventArgs
    {
        public DeviceDiscoveredEventArgs(IRokuDevice device)
        {
            this.Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        public bool CancelDiscovery { get; set; }

        public IRokuDevice Device { get; }
    }
}
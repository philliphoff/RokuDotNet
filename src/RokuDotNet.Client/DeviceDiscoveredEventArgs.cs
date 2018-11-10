using System;

namespace RokuDotNet.Client
{
    public class DeviceDiscoveredEventArgs : EventArgs
    {
        public DeviceDiscoveredEventArgs(DiscoveredDeviceContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool CancelDiscovery { get; set; }

        public DiscoveredDeviceContext Context { get; }
    }
}
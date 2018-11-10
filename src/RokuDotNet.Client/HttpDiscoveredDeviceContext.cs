namespace RokuDotNet.Client
{
    public sealed class HttpDiscoveredDeviceContext : DiscoveredDeviceContext
    {
        public HttpDiscoveredDeviceContext(IHttpRokuDevice device, string serialNumber)
            : base(device, serialNumber)
        {
            this.Device = device;
        }

        public new IHttpRokuDevice Device { get; }
    }
}
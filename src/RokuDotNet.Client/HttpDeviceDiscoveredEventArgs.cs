namespace RokuDotNet.Client
{
    public sealed class HttpDeviceDiscoveredEventArgs : DeviceDiscoveredEventArgs
    {
        public HttpDeviceDiscoveredEventArgs(HttpDiscoveredDeviceContext context)
            : base(context)
        {
            this.Context = context;
        }

        public new HttpDiscoveredDeviceContext Context { get; }
    }
}
using RokuDotNet.Client;
using System;
using System.Threading.Tasks;

namespace RokuDotNet.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var discoveryClient = new UdpRokuDeviceDiscoveryClient();

            var device = await discoveryClient.DiscoverFirstDeviceAsync();

            var result = await device.Query.GetAppsAsync();
        }
    }
}

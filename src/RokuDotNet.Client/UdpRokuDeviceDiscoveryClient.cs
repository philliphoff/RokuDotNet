using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RokuDotNet.Client
{
    public sealed class UdpRokuDeviceDiscoveryClient : IRokuDeviceDiscoveryClient
    {
        private EventHandler<DeviceDiscoveredEventArgs> baseDeviceDiscovered;

        public event EventHandler<HttpDeviceDiscoveredEventArgs> DeviceDiscovered;

        #region IRokuDeviceDiscoveryClient Members

        event EventHandler<DeviceDiscoveredEventArgs> IRokuDeviceDiscoveryClient.DeviceDiscovered
        {
            add
            {
                this.baseDeviceDiscovered += value;
            }

            remove
            {
                this.baseDeviceDiscovered -= value;
            }
        }

        public Task DiscoverDevicesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.DiscoverDevicesAsync(null, cancellationToken);
        }

        public async Task DiscoverDevicesAsync(Func<DiscoveredDeviceContext, Task<bool>> onDeviceDiscovered, CancellationToken cancellationToken = default(CancellationToken))
        {
            string discoverRequest =
"M-SEARCH * HTTP/1.1\n" +
"Host: 239.255.255.250:1900\n" +
"Man: \"ssdp:discover\"\n" +
"ST: roku:ecp\n" +
"";
            var bytes = Encoding.UTF8.GetBytes(discoverRequest);

            using (var udpClient = new UdpClient())
            {
                await udpClient.SendAsync(bytes, bytes.Length, "239.255.255.250", 1900).ConfigureAwait(false);

                while (!cancellationToken.IsCancellationRequested)
                {
                    var receiveTask = Task.Run(
                        async () =>
                        {
                            try
                            {
                                return await udpClient.ReceiveAsync().ConfigureAwait(false);
                            }
                            catch (ObjectDisposedException)
                            {
                                // NOTE: We assume that a disposal exception is an attempt to cancel an
                                //       outstanding ReceiveAsync() by closing the socket (disposing the
                                //       UdpClient).

                                throw new OperationCanceledException();
                            }
                        });

                    var cancellationTask = Task.Delay(TimeSpan.FromMilliseconds(-1), cancellationToken);

                    var completedTask = await Task.WhenAny(receiveTask, cancellationTask).ConfigureAwait(false);

                    if (completedTask == cancellationTask)
                    {
                        // NOTE: We allow the OperationCanceledException to bubble up, causing disposal of the
                        //       UdpClient which would force any pending ReceiveAsync() to throw an 
                        //       ObjectDisposedException.

                        await cancellationTask.ConfigureAwait(false);

                        return;
                    }

                    var rawResponse = await receiveTask.ConfigureAwait(false);
                    var response = await ParseResponseAsync(rawResponse.Buffer).ConfigureAwait(false);

                    if (response.StatusCode == 200
                        && response.Headers.TryGetValue("ST", out string stHeader)
                        && stHeader == "roku:ecp"
                        && response.Headers.TryGetValue("LOCATION", out string location)
                        && Uri.TryCreate(location, UriKind.Absolute, out Uri locationUri)
                        && response.Headers.TryGetValue("USN", out string serialNumber))
                    {
                        var device = new HttpRokuDevice(serialNumber, locationUri);

                        bool cancelDiscovery = false;
                        var context = new HttpDiscoveredDeviceContext(device, serialNumber);

                        if (onDeviceDiscovered != null)
                        {
                            cancelDiscovery = await onDeviceDiscovered(context).ConfigureAwait(false);
                        }

                        var args = new HttpDeviceDiscoveredEventArgs(context)
                        {
                            CancelDiscovery = cancelDiscovery
                        };

                        this.DeviceDiscovered?.Invoke(this, args);

                        cancelDiscovery = args.CancelDiscovery;

                        var baseArgs = new DeviceDiscoveredEventArgs(context)
                        {
                            CancelDiscovery = cancelDiscovery
                        };

                        this.baseDeviceDiscovered?.Invoke(this, baseArgs);

                        cancelDiscovery = baseArgs.CancelDiscovery;

                        if (cancelDiscovery)
                        {
                            return;
                        }
                    }
                }
            }
        }

        #endregion

        private async Task<HttpResponse> ParseResponseAsync(byte[] response)
        {
            using (var stream = new MemoryStream(response))
            using (var reader = new StreamReader(stream))
            {
                string statusLine = await reader.ReadLineAsync().ConfigureAwait(false);
                string[] splitStatusLine = statusLine.Split(new[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);
                
                string httpVersion = splitStatusLine[0];
                int statusCode = Int32.Parse(splitStatusLine[1]);
                string statusMessage = splitStatusLine[2];

                var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync().ConfigureAwait(false);
                    var colonIndex = line.IndexOf(':');

                    if (colonIndex >= 0)
                    {
                        string header = line.Substring(0, colonIndex).Trim();
                        string value = line.Substring(colonIndex + 1).Trim();

                        headers[header] = value;
                    }
                }

                return new HttpResponse
                {
                    HttpVersion = httpVersion,
                    StatusCode = statusCode,
                    StatusMessage = statusMessage,
                    Headers = headers
                };
            }
        }

        private sealed class HttpResponse
        {
            public string HttpVersion { get; set; }

            public int StatusCode { get; set; }

            public string StatusMessage { get; set; }

            public IDictionary<string, string> Headers { get; set; }
        }
    }
}
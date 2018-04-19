using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public sealed class RokuDevice : IRokuDevice, IRokuDeviceQueryApi
    {
        public RokuDevice(Uri location, string serialNumber)
        {
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
        }

        #region IRokuDevice Members

        public Uri Location { get; }

        public string SerialNumber { get; }

        public IRokuDeviceQueryApi QueryApi => this;

        #endregion

        #region IRokuDeviceQueryApi Members

        Task<GetActiveAppResult> IRokuDeviceQueryApi.GetActiveAppAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetAsync<GetActiveAppResult>("query/active-app");
        }

        Task<GetAppsResult> IRokuDeviceQueryApi.GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetAsync<GetAppsResult>("query/apps");
        }

        Task<GetTvChannelsResult> IRokuDeviceQueryApi.GetTvChannelsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetAsync<GetTvChannelsResult>("query/tv-channels");
        }

        Task<GetActiveTvChannelResult> IRokuDeviceQueryApi.GetActiveTvChannelAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return this.GetAsync<GetActiveTvChannelResult>("query/tv-active-channel");
        }

        #endregion

        private async Task<T> GetAsync<T>(string relativeUrl)
        {
            var httpClient = new HttpClient();

            // NOTE: Roku returns "Content-Type: text/xml; charset="utf-8"".
            //       The quotes surrounding the encoding are problematic for 
            //       HttpClient.GetStringAsync(), so use GetByteArrayAsync().

            using (var stream = await httpClient.GetStreamAsync(new Uri(this.Location, relativeUrl)).ConfigureAwait(false))
            {
                return Deserialize<T>(stream);
            }            
        }

        private static T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(stream);
        }
    }
}
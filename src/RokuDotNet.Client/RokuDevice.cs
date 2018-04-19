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

        async Task<GetActiveAppResult> IRokuDeviceQueryApi.GetActiveAppAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var httpClient = new HttpClient();

            // NOTE: Roku returns "Content-Type: text/xml; charset="utf-8"".
            //       The quotes surrounding the encoding are problematic for 
            //       HttpClient.GetStringAsync(), so use GetByteArrayAsync().

            using (var stream = await httpClient.GetStreamAsync(new Uri(this.Location, "query/active-app")).ConfigureAwait(false))
            {
                return Deserialize<GetActiveAppResult>(stream);
            }
        }

        async Task<GetAppsResult> IRokuDeviceQueryApi.GetAppsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var httpClient = new HttpClient();

            // NOTE: Roku returns "Content-Type: text/xml; charset="utf-8"".
            //       The quotes surrounding the encoding are problematic for 
            //       HttpClient.GetStringAsync(), so use GetByteArrayAsync().

            using (var stream = await httpClient.GetStreamAsync(new Uri(this.Location, "query/apps")).ConfigureAwait(false))
            {
                return Deserialize<GetAppsResult>(stream);
            }
        }

        #endregion

        private static T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(stream);
        }
    }
}
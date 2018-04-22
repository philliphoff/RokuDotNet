using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public sealed class RokuDevice : IRokuDevice, IRokuDeviceInputApi, IRokuDeviceQueryApi
    {
        public RokuDevice(Uri location, string serialNumber)
        {
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
            this.SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
        }

        #region IRokuDevice Members

        public IRokuDeviceInputApi InputApi => this;

        public Uri Location { get; }

        public string SerialNumber { get; }

        public IRokuDeviceQueryApi QueryApi => this;

        #endregion

        #region IRokuDeviceInputApi Members

        Task IRokuDeviceInputApi.KeyDownAsync(SpecialKeys key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keydown", key, cancellationToken);
        }

        Task IRokuDeviceInputApi.KeyDownAsync(char key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keydown", key, cancellationToken);
        }

        Task IRokuDeviceInputApi.KeyPressAsync(SpecialKeys key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keypress", key, cancellationToken);
        }

        Task IRokuDeviceInputApi.KeyPressAsync(char key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keypress", key, cancellationToken);
        }

        Task IRokuDeviceInputApi.KeyUpAsync(SpecialKeys key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keyup", key, cancellationToken);
        }

        Task IRokuDeviceInputApi.KeyUpAsync(char key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keyup", key, cancellationToken);
        }

        private Task KeyInputAsync(string inputType, SpecialKeys key, CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            return client.PostAsync(new Uri(this.Location, $"{inputType}/{InputEncoding.EncodeSpecialKey(key)}"), new ByteArrayContent(new byte[] {}), cancellationToken);
        }

        private Task KeyInputAsync(string inputType, char key, CancellationToken cancellationToken)
        {
            var client = new HttpClient();

            return client.PostAsync(new Uri(this.Location, $"{inputType}/{InputEncoding.EncodeChar(key)}"), new ByteArrayContent(new byte[] {}), cancellationToken);
        }

        #endregion

        #region IRokuDeviceQueryApi Members

        Task<GetActiveAppResult> IRokuDeviceQueryApi.GetActiveAppAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetActiveAppResult>("query/active-app");
        }

        Task<GetAppsResult> IRokuDeviceQueryApi.GetAppsAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetAppsResult>("query/apps");
        }

        Task<GetTvChannelsResult> IRokuDeviceQueryApi.GetTvChannelsAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetTvChannelsResult>("query/tv-channels");
        }

        Task<GetActiveTvChannelResult> IRokuDeviceQueryApi.GetActiveTvChannelAsync(CancellationToken cancellationToken)
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
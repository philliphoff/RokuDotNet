using System;
using System.Collections.Generic;
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
    public sealed class HttpRokuDevice : IHttpRokuDevice, IRokuDeviceInput, IRokuDeviceQuery
    {
        private readonly HttpClient client;

        public HttpRokuDevice(string id, Uri location, HttpMessageHandler handler = null)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.Location = location ?? throw new ArgumentNullException(nameof(location));

            this.client = handler != null ? new HttpClient(handler) : new HttpClient();
        }

        #region IHttpRokuDevice Members

        public Uri Location { get; }

        #endregion

        #region IRokuDevice Members

        public IRokuDeviceInput Input => this;

        public string Id { get; }

        public IRokuDeviceQuery Query => this;

        #endregion

        #region IRokuDeviceInput Members

        Task IRokuDeviceInput.KeyDownAsync(PressedKey key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keydown", key, cancellationToken);
        }

        Task IRokuDeviceInput.KeyPressAsync(PressedKey key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keypress", key, cancellationToken);
        }

        async Task IRokuDeviceInput.KeyPressAsync(IEnumerable<PressedKey> keys, CancellationToken cancellationToken)
        {
            var input = (IRokuDeviceInput)this;

            foreach (var key in keys)
            {
                await input.KeyPressAsync(key, cancellationToken).ConfigureAwait(false);
            }
        }

        Task IRokuDeviceInput.KeyUpAsync(PressedKey key, CancellationToken cancellationToken)
        {
            return this.KeyInputAsync("keyup", key, cancellationToken);
        }

        #endregion

        #region IRokuDeviceQuery Members

        Task<GetActiveAppResult> IRokuDeviceQuery.GetActiveAppAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetActiveAppResult>("query/active-app");
        }

        Task<GetActiveTvChannelResult> IRokuDeviceQuery.GetActiveTvChannelAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetActiveTvChannelResult>("query/tv-active-channel");
        }

        Task<GetAppsResult> IRokuDeviceQuery.GetAppsAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetAppsResult>("query/apps");
        }

        Task<DeviceInfo> IRokuDeviceQuery.GetDeviceInfoAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<DeviceInfo>("query/device-info");
        }

        Task<GetTvChannelsResult> IRokuDeviceQuery.GetTvChannelsAsync(CancellationToken cancellationToken)
        {
            return this.GetAsync<GetTvChannelsResult>("query/tv-channels");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.client.Dispose();
        }

        #endregion

        private Task KeyInputAsync(string inputType, PressedKey key, CancellationToken cancellationToken)
        {
            string encodedKey = key.Match(InputEncoding.EncodeSpecialKey, InputEncoding.EncodeChar);
            return this.client.PostAsync(new Uri(this.Location, $"{inputType}/{encodedKey}"), new ByteArrayContent(new byte[] {}), cancellationToken);
        }

        private async Task<T> GetAsync<T>(string relativeUrl)
        {
            // NOTE: Roku returns "Content-Type: text/xml; charset="utf-8"".
            //       The quotes surrounding the encoding are problematic for 
            //       HttpClient.GetStringAsync(), so use GetByteArrayAsync().

            using (var stream = await this.client.GetStreamAsync(new Uri(this.Location, relativeUrl)).ConfigureAwait(false))
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
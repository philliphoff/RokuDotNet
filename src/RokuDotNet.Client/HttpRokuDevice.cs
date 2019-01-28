using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using RokuDotNet.Client.Apps;
using RokuDotNet.Client.Input;
using RokuDotNet.Client.Query;

namespace RokuDotNet.Client
{
    public sealed class HttpRokuDevice : IHttpRokuDevice, IRokuDeviceApps, IRokuDeviceInput, IRokuDeviceQuery
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

        public IRokuDeviceApps Apps => this;

        public string Id { get; }

        public IRokuDeviceInput Input => this;

        public IRokuDeviceQuery Query => this;

        #endregion

        #region IRokuDeviceApps Members

        Task IRokuDeviceApps.InstallAppAsync(string appId, CancellationToken cancellationToken )
        {
            return this.InstallAppCoreAsync(appId, null, cancellationToken);
        }
    
        Task IRokuDeviceApps.InstallAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            return this.InstallAppCoreAsync(appId, parameters, cancellationToken);
        }

        Task IRokuDeviceApps.LaunchAppAsync(string appId, CancellationToken cancellationToken)
        {
            return this.LaunchAppCoreAsync(appId, null, cancellationToken);
        }

        Task IRokuDeviceApps.LaunchAppAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            return this.LaunchAppCoreAsync(appId, parameters, cancellationToken);
        }

        Task IRokuDeviceApps.LaunchTvInputAsync(CancellationToken cancellationToken)
        {
            return this.LaunchTvInputCoreAsync(null, cancellationToken);
        }

        Task IRokuDeviceApps.LaunchTvInputAsync(string channel, CancellationToken cancellationToken)
        {
            return this.LaunchTvInputCoreAsync(new Dictionary<string, string>{ { "ch", channel } }, cancellationToken);
        }

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

        private Task InstallAppCoreAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            string relativeUrl = $"install/{appId}{this.CreateQueryString(parameters)}";

            return this.PostAsync(relativeUrl, cancellationToken);
        }

        private Task LaunchAppCoreAsync(string appId, IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            string relativeUrl = $"launch/{appId}{this.CreateQueryString(parameters)}";

            return this.PostAsync(relativeUrl, cancellationToken);
        }

        private Task LaunchTvInputCoreAsync(IDictionary<string, string> parameters, CancellationToken cancellationToken)
        {
            return this.LaunchAppCoreAsync("tvinput.dtv", parameters, cancellationToken);
        }

        private string CreateQueryString(IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Any())
            {
                return "?" + String.Join("&", parameters.Select(parameter => $"{HttpUtility.UrlEncode(parameter.Key)}={HttpUtility.UrlEncode(parameter.Value)}"));
            }

            return String.Empty;
        }

        private Task KeyInputAsync(string inputType, PressedKey key, CancellationToken cancellationToken)
        {
            string encodedKey = key.Match(InputEncoding.EncodeSpecialKey, InputEncoding.EncodeChar);
            return this.PostAsync($"{inputType}/{encodedKey}", cancellationToken);
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

        private async Task PostAsync(string relativeUrl, CancellationToken cancellationToken)
        {
            var result = await this.client.PostAsync(new Uri(this.Location, relativeUrl), new ByteArrayContent(new byte[] {}), cancellationToken).ConfigureAwait(false);

            result.EnsureSuccessStatusCode();
        }

        private static T Deserialize<T>(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(stream);
        }
    }
}
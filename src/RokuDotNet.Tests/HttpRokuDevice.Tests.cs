using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using RokuDotNet.Client;
using RokuDotNet.Client.Input;
using Xunit;
using System.Net;
using System.Threading;
using System.Linq.Expressions;

namespace RokuDotNet.Tests
{
    public sealed class HttpRokuDeviceTests
    {
        [Fact]
        public Task InputKeyPressLiteralKey()
        {
            return this.InputKeyPressTest('z', "Lit_z");
        }

        [Fact]
        public Task InputKeyPressSpecialKey()
        {
            return this.InputKeyPressTest(SpecialKeys.VolumeUp, "VolumeUp");
        }

        private async Task InputKeyPressTest(PressedKey key, string relativeUrl)
        {
            var handler = new HttpMessageHandlerMock(MockBehavior.Strict);

            Expression<Func<HttpRequestMessage, bool>> volumeUpRequest =
                message => message.Method == HttpMethod.Post
                    && message.RequestUri == new Uri(new Uri("http://localhost/keypress/"), relativeUrl);

            handler.SetupSendAsync(volumeUpRequest, new HttpResponseMessage(HttpStatusCode.OK));

            var client = new HttpRokuDevice("deviceId", new Uri("http://localhost"), handler.Object);

            await client.Input.KeyPressAsync(key);

            handler.VerifySendAsync(Times.Exactly(1));
        }

        [Fact]
        public async Task InputKeyPressMultipleKeys()
        {
            var handler = new HttpMessageHandlerMock(MockBehavior.Strict);

            Expression<Func<HttpRequestMessage, bool>> volumeUpRequest =
                message => message.Method == HttpMethod.Post
                    && message.RequestUri == new Uri("http://localhost/keypress/VolumeUp");

            handler.SetupSendAsync(volumeUpRequest, new HttpResponseMessage(HttpStatusCode.OK));

            Expression<Func<HttpRequestMessage, bool>> zRequest =
                message => message.Method == HttpMethod.Post
                    && message.RequestUri == new Uri("http://localhost/keypress/Lit_z");

            handler.SetupSendAsync(zRequest, new HttpResponseMessage(HttpStatusCode.OK));

            var client = new HttpRokuDevice("deviceId", new Uri("http://localhost"), handler.Object);

            await client.Input.KeyPressAsync(new PressedKey[] { SpecialKeys.VolumeUp, 'z' });

            handler.VerifySendAsync(Times.Exactly(2));
        }
    }
}
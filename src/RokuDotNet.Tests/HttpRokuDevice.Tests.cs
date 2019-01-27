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
        public Task InputKeyDownLiteralKey()
        {
            return this.InputKeyPressTest(input => input.KeyDownAsync('z'), "keydown/Lit_z");
        }

        [Fact]
        public Task InputKeyDownSpecialKey()
        {
            return this.InputKeyPressTest(input => input.KeyDownAsync(SpecialKeys.VolumeUp), "keydown/VolumeUp");
        }

        [Fact]
        public Task InputKeyPressLiteralKey()
        {
            return this.InputKeyPressTest(input => input.KeyPressAsync('z'), "keypress/Lit_z");
        }

        [Fact]
        public Task InputKeyPressSpecialKey()
        {
            return this.InputKeyPressTest(input => input.KeyPressAsync(SpecialKeys.VolumeUp), "keypress/VolumeUp");
        }

        [Fact]
        public Task InputKeyUpLiteralKey()
        {
            return this.InputKeyPressTest(input => input.KeyUpAsync('z'), "keyup/Lit_z");
        }

        [Fact]
        public Task InputKeyUpSpecialKey()
        {
            return this.InputKeyPressTest(input => input.KeyUpAsync(SpecialKeys.VolumeUp), "keyup/VolumeUp");
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

        private async Task InputKeyPressTest(Func<IRokuDeviceInput, Task> inputFunc, string relativeUrl)
        {
            var handler = new HttpMessageHandlerMock(MockBehavior.Strict);

            Expression<Func<HttpRequestMessage, bool>> volumeUpRequest =
                message => message.Method == HttpMethod.Post
                    && message.RequestUri == new Uri(new Uri("http://localhost/"), relativeUrl);

            handler.SetupSendAsync(volumeUpRequest, new HttpResponseMessage(HttpStatusCode.OK));

            var client = new HttpRokuDevice("deviceId", new Uri("http://localhost"), handler.Object);

            await inputFunc(client.Input);

            handler.VerifySendAsync(Times.Exactly(1));
        }
    }
}
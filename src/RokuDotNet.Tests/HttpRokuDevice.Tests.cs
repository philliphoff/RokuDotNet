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
using System.Collections.Generic;

namespace RokuDotNet.Tests
{
    public sealed class HttpRokuDeviceTests
    {
        [Fact]
        public Task GetDeviceInfoTest()
        {
            return this.HttpGetTest(
                device => device.GetDeviceInfoAsync(),
                "query/device-info",
                @"<device-info>
                    <device-id>deviceId</device-id>
                </device-info>",
                result => Assert.Equal("deviceId", result.DeviceId));
        }

        [Fact]
        public Task AppsGetActiveAppTest()
        {
            return this.HttpGetTest(
                device => device.Apps.GetActiveAppAsync(),
                "query/active-app",
                @"<active-app>
                    <app>appName</app>
                </active-app>",
                result => Assert.Equal("appName", result.ActiveApp.Name));
        }

        [Fact]
        public Task AppsGetActiveTvChannelTest()
        {
            return this.HttpGetTest(
                device => device.Apps.GetActiveTvChannelAsync(),
                "query/tv-active-channel",
                @"<tv-channel>
                    <channel>
                        <name>channelName</name>
                    </channel>
                </tv-channel>",
                result => Assert.Equal("channelName", result.ActiveChannel.Name));
        }

        [Fact]
        public Task AppsGetAppsTest()
        {
            return this.HttpGetTest(
                device => device.Apps.GetAppsAsync(),
                "query/apps",
                @"<apps>
                    <app>app1</app>
                    <app>app2</app>
                </apps>",
                result =>
                {
                    Assert.Equal(2, result.Apps.Length);
                    Assert.Equal("app1", result.Apps[0].Name);
                    Assert.Equal("app2", result.Apps[1].Name);
                });
        }

        [Fact]
        public Task AppsGetTvChannelsTest()
        {
            return this.HttpGetTest(
                device => device.Apps.GetTvChannelsAsync(),
                "query/tv-channels",
                @"<tv-channels>
                    <channel>
                        <name>app1</name>
                    </channel>
                    <channel>
                        <name>app2</name>
                    </channel>
                </tv-channels>",
                result =>
                {
                    Assert.Equal(2, result.Channels.Length);
                    Assert.Equal("app1", result.Channels[0].Name);
                    Assert.Equal("app2", result.Channels[1].Name);
                });
        }

        [Fact]
        public Task AppsInstallAppTest()
        {
            return this.HttpPostTest(device => device.Apps.InstallAppAsync("appId"), "install/appId");
        }

        [Fact]
        public Task AppsInstallAppWithParametersTest()
        {
            var parameters = new Dictionary<string, string> 
            { 
                { "one#", "value%" },
                { "two%", "value&" }
            };

            return this.HttpPostTest(device => device.Apps.InstallAppAsync("appId", parameters), "install/appId?one%23=value%25&two%25=value%26");
        }

        [Fact]
        public Task AppsLaunchAppTest()
        {
            return this.HttpPostTest(device => device.Apps.LaunchAppAsync("appId"), "launch/appId");
        }

        [Fact]
        public Task AppsLaunchAppWithParametersTest()
        {
            var parameters = new Dictionary<string, string> 
            { 
                { "one#", "value%" },
                { "two%", "value&" }
            };

            return this.HttpPostTest(device => device.Apps.LaunchAppAsync("appId", parameters), "launch/appId?one%23=value%25&two%25=value%26");
        }

        [Fact]
        public Task AppsTvInputTest()
        {
            return this.HttpPostTest(device => device.Apps.LaunchTvInputAsync(), "launch/tvinput.dtv");
        }

        [Fact]
        public Task AppsTvInputWithChannelTest()
        {
            return this.HttpPostTest(device => device.Apps.LaunchTvInputAsync("one#"), "launch/tvinput.dtv?ch=one%23");
        }

        [Fact]
        public Task InputKeyDownLiteralKey()
        {
            return this.HttpPostTest(device => device.Input.KeyDownAsync('z'), "keydown/Lit_z");
        }

        [Fact]
        public Task InputKeyDownSpecialKey()
        {
            return this.HttpPostTest(device => device.Input.KeyDownAsync(SpecialKeys.VolumeUp), "keydown/VolumeUp");
        }

        [Fact]
        public Task InputKeyPressLiteralKey()
        {
            return this.HttpPostTest(device => device.Input.KeyPressAsync('z'), "keypress/Lit_z");
        }

        [Fact]
        public Task InputKeyPressSpecialKey()
        {
            return this.HttpPostTest(device => device.Input.KeyPressAsync(SpecialKeys.VolumeUp), "keypress/VolumeUp");
        }

        [Fact]
        public Task InputKeyUpLiteralKey()
        {
            return this.HttpPostTest(device => device.Input.KeyUpAsync('z'), "keyup/Lit_z");
        }

        [Fact]
        public Task InputKeyUpSpecialKey()
        {
            return this.HttpPostTest(device => device.Input.KeyUpAsync(SpecialKeys.VolumeUp), "keyup/VolumeUp");
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

        private async Task HttpGetTest<TResult>(Func<IRokuDevice, Task<TResult>> inputFunc, string relativeUrl, string xmlResult, Action<TResult> assertResult)
        {
            var handler = new HttpMessageHandlerMock(MockBehavior.Strict);

            Expression<Func<HttpRequestMessage, bool>> volumeUpRequest =
                message => message.Method == HttpMethod.Get
                    && message.RequestUri == new Uri(new Uri("http://localhost/"), relativeUrl);

            handler.SetupSendAsync(volumeUpRequest, new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(xmlResult) });

            var device = new HttpRokuDevice("deviceId", new Uri("http://localhost"), handler.Object);

            var result = await inputFunc(device);

            handler.VerifySendAsync(Times.Exactly(1));

            assertResult(result);
        }

        private async Task HttpPostTest(Func<IRokuDevice, Task> inputFunc, string relativeUrl)
        {
            var handler = new HttpMessageHandlerMock(MockBehavior.Strict);

            Expression<Func<HttpRequestMessage, bool>> volumeUpRequest =
                message => message.Method == HttpMethod.Post
                    && message.RequestUri == new Uri(new Uri("http://localhost/"), relativeUrl);

            handler.SetupSendAsync(volumeUpRequest, new HttpResponseMessage(HttpStatusCode.OK));

            var device = new HttpRokuDevice("deviceId", new Uri("http://localhost"), handler.Object);

            await inputFunc(device);

            handler.VerifySendAsync(Times.Exactly(1));
        }
    }
}
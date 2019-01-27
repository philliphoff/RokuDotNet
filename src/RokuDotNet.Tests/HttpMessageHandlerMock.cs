using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace RokuDotNet.Tests
{
    internal sealed class HttpMessageHandlerMock : Mock<HttpMessageHandler>
    {
        public HttpMessageHandlerMock()
            : base()
        {            
        }

        public HttpMessageHandlerMock(MockBehavior behavior)
            : base(behavior)
        {            
        }

        public void SetupSendAsync(Expression<Func<HttpRequestMessage, bool>> requestMatch, HttpResponseMessage response)
        {
            this
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(requestMatch), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(response));
        }

        public void VerifySendAsync(Times times)
        {
            this.Protected().Verify("SendAsync", times, ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }
    }
}
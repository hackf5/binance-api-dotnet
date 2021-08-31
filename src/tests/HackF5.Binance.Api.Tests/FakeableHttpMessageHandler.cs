namespace HackF5.Binance.Api.Tests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using FakeItEasy;

    public abstract class FakeableHttpMessageHandler : HttpMessageHandler
    {
        public static FakeableHttpMessageHandler CreateFake() =>
            A.Fake<FakeableHttpMessageHandler>(o => o.CallsBaseMethods());

        public abstract Task<HttpResponseMessage> SendAsyncCore(HttpRequestMessage request);

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.SendAsyncCore(request);
        }
    }
}
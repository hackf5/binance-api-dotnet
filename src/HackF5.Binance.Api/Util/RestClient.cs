namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Request.Rest;

    public sealed class RestClient : IRestClient
    {
        private const int IpBanStatusCode = 418;

        private const string RetryAfterHeaderKey = "Retry-After";

        private const string DummyUriString = "http://hackf5.io/";

        private readonly IApiHttpClientFactory _clientFactory;

        private readonly IRequestSemaphore _semaphore;

        private readonly ITemporalServices _temporal;

        public RestClient(
            IApiHttpClientFactory clientFactory,
            IRequestSemaphore semaphore,
            ITemporalServices temporal)
        {
            this._clientFactory = clientFactory;
            this._semaphore = semaphore;
            this._temporal = temporal;
        }

        public async Task<string> GetResponseAsync(
            RestRequest request,
            int maxAttempts,
            CancellationToken cancellation)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var uri = new Uri(DummyUriString).MakeRelativeUri(
                new UriBuilder(DummyUriString) { Path = request.Path, Query = request.Query }.Uri);

            using var client = this._clientFactory.CreateClient();

            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                await this._semaphore.WaitAsync(request, cancellation);
                var response = await client.GetAsync(uri, cancellation);

                switch ((int)response.StatusCode)
                {
                    case (int)HttpStatusCode.TooManyRequests:
                    case IpBanStatusCode:
                        var seconds = Convert.ToDouble(
                            response.Headers.First(h => h.Key == RetryAfterHeaderKey).Value.First()!,
                            CultureInfo.InvariantCulture);

                        await this._temporal.Delay(TimeSpan.FromSeconds(seconds), cancellation);
                        continue;
                    default:
                        response.EnsureSuccessStatusCode();
                        break;
                }

                return await response.Content.ReadAsStringAsync(cancellation);
            }

            throw new InvalidOperationException($"Too many attempts. Max attempts = {maxAttempts}.");
        }
    }
}
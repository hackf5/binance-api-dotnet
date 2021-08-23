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
        private const int DefaultMaxAttempts = 10;

        private const int IpBan = 418;

        private const string RetryAfterHeaderKey = "Retry-After";

        private readonly ApiHttpClientFactory _clientFactory;

        private readonly RequestSemaphore _semaphore;

        public RestClient(ApiHttpClientFactory clientFactory, RequestSemaphore semaphore)
        {
            this._clientFactory = clientFactory;
            this._semaphore = semaphore;
        }

        public async Task<string> GetRequestAsync(
            RestRequest request,
            int maxAttempts = DefaultMaxAttempts,
            CancellationToken cancellation = default)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var uri = new Uri("http://hackf5.io/").MakeRelativeUri(
                new UriBuilder("http://hackf5.io/") { Path = request.Path, Query = request.Query }.Uri);

            using var client = this._clientFactory.CreateClient();

            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                await this._semaphore.WaitAsync(request, cancellation);
                var response = await client.GetAsync(uri, cancellation);

                switch ((int)response.StatusCode)
                {
                    case (int)HttpStatusCode.TooManyRequests:
                    case IpBan:
                        var seconds = Convert.ToDouble(
                            response.Headers.First(h => h.Key == RetryAfterHeaderKey).Value.First()!,
                            CultureInfo.InvariantCulture);

                        await Task.Delay(TimeSpan.FromSeconds(seconds), cancellation);
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
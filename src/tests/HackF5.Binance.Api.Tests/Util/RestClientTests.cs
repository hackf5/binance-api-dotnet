namespace HackF5.Binance.Api.Tests.Util
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using FakeItEasy;

    using HackF5.Binance.Api.Request.Rest;
    using HackF5.Binance.Api.Util;

    using Xunit;

    public sealed class RestClientTests : IDisposable
    {
        private const int OneAttempt = 1;

        private const int TwoAttempts = 2;

        private const int ThreeAttempts = 3;

        private readonly FakeableHttpMessageHandler _handler = FakeableHttpMessageHandler.CreateFake();

        private readonly IRequestSemaphore _semaphore = A.Fake<IRequestSemaphore>();

        private readonly ITemporalServices _temporal = A.Fake<ITemporalServices>();

        private readonly IApiHttpClientFactory _httpClientFactory = A.Fake<IApiHttpClientFactory>();

        private readonly HttpClient _httpClient;

        private readonly RestClient _client;

        public RestClientTests()
        {
            this._httpClient = new HttpClient(this._handler) { BaseAddress = new("https://hackf5.io") };
            A.CallTo(() => this._httpClientFactory.CreateClient()).Returns(this._httpClient);
            this._client = new RestClient(this._httpClientFactory, this._semaphore, this._temporal);
        }

        [Fact]
        public async Task GetResponseAsync_NullRequest_Throws()
        {
            // Given
            // When
            // Then
            await Assert.ThrowsAsync<ArgumentNullException>(
                "request",
                () => this._client.GetResponseAsync(null!, OneAttempt, CancellationToken.None));
        }

        [Fact]
        public async Task GetResponseAsync_ClientSuccess_ReturnsContentString()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");
            using var response = this.SetupResponseFake(HttpStatusCode.OK, "Jason[sic.]");

            // When
            var result = await this._client.GetResponseAsync(
                request, OneAttempt, CancellationToken.None);

            // Then
            Assert.Equal("Jason[sic.]", result);

            var expectedUri = new Uri("https://hackf5.io/foo/bar?aa=1&bb=2");
            A.CallTo(
               () => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
               .MustHaveHappenedOnceExactly();
            A.CallTo(
                () => this._handler.SendAsyncCore(
                    A<HttpRequestMessage>.That.Matches(r => r.RequestUri == expectedUri)))
                .MustHaveHappenedOnceExactly();
            A.CallTo(
                () => this._semaphore.WaitAsync(request, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(
                () => this._temporal.Delay(A<TimeSpan>._, A<CancellationToken>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task GetResponseAsync_ClientFails_Throws()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");
            using var response = this.SetupResponseFake(HttpStatusCode.BadRequest, "Jason[sic.]");

            // When
            var ex = await Assert.ThrowsAsync<HttpRequestException>(() => this._client.GetResponseAsync(
                request, TwoAttempts, CancellationToken.None));

            // Then
            Assert.Equal(HttpStatusCode.BadRequest, ex.StatusCode);

            A.CallTo(
               () => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
               .MustHaveHappenedOnceExactly();
            A.CallTo(
                () => this._semaphore.WaitAsync(request, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(
               () => this._temporal.Delay(A<TimeSpan>._, A<CancellationToken>._))
               .MustNotHaveHappened();
        }

        [Fact]
        public async Task GetResponseAsync_ClientHasMadeTooManyRequests_WaitsForDelay()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");

            using var responseSuccess = this.SetupResponseFake(HttpStatusCode.OK, "Jason[sic.]");
            using var responseFail = this.SetupResponseFake(HttpStatusCode.TooManyRequests, "-");
            responseFail.Headers.Add("Retry-After", "4");

            // When
            var result = await this._client.GetResponseAsync(
                request, TwoAttempts, CancellationToken.None);

            // Then
            Assert.Equal("Jason[sic.]", result);

            A.CallTo(
               () => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
               .MustHaveHappenedTwiceExactly();
            A.CallTo(
                () => this._semaphore.WaitAsync(request, A<CancellationToken>._))
                .MustHaveHappenedTwiceExactly();
            var delay = TimeSpan.FromSeconds(4);
            A.CallTo(
               () => this._temporal.Delay(delay, A<CancellationToken>._))
               .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetResponseAsync_ClientHasMadeTooManyRequestsTwice_WaitsForDelay()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");

            using var responseSuccess = this.SetupResponseFake(HttpStatusCode.OK, "Jason[sic.]");
            using var responseFail1 = this.SetupResponseFake(HttpStatusCode.TooManyRequests, "-");
            responseFail1.Headers.Add("Retry-After", "4");
            using var responseFail2 = this.SetupResponseFake(HttpStatusCode.TooManyRequests, "-");
            responseFail2.Headers.Add("Retry-After", "2");

            // When
            var result = await this._client.GetResponseAsync(
                request, ThreeAttempts, CancellationToken.None);

            // Then
            Assert.Equal("Jason[sic.]", result);

            A.CallTo(
               () => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
               .MustHaveHappened(3, Times.Exactly);
            A.CallTo(
                () => this._semaphore.WaitAsync(request, A<CancellationToken>._))
               .MustHaveHappened(3, Times.Exactly);

            var delay4 = TimeSpan.FromSeconds(4);
            A.CallTo(
               () => this._temporal.Delay(delay4, A<CancellationToken>._))
               .MustHaveHappenedOnceExactly();
            var delay2 = TimeSpan.FromSeconds(2);
            A.CallTo(
               () => this._temporal.Delay(delay2, A<CancellationToken>._))
               .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetResponseAsync_ClientHasIpBan_WaitsForDelay()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");

            using var responseSuccess = this.SetupResponseFake(HttpStatusCode.OK, "Jason[sic.]");
            using var responseFail = this.SetupResponseFake((HttpStatusCode)418, "-");
            responseFail.Headers.Add("Retry-After", "4");

            // When
            var result = await this._client.GetResponseAsync(
                request, TwoAttempts, CancellationToken.None);

            // Then
            Assert.Equal("Jason[sic.]", result);

            A.CallTo(
               () => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
               .MustHaveHappenedTwiceExactly();
            A.CallTo(
                () => this._semaphore.WaitAsync(request, A<CancellationToken>._))
                .MustHaveHappenedTwiceExactly();
            var delay = TimeSpan.FromSeconds(4);
            A.CallTo(
               () => this._temporal.Delay(delay, A<CancellationToken>._))
               .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetResponseAsync_ClientExceedsAttempts_Throws()
        {
            // Given
            var request = CreateRequestFake(path: "/foo/bar", query: "aa=1&bb=2");

            using var responseFail = this.SetupResponseFake(HttpStatusCode.TooManyRequests, "-");
            responseFail.Headers.Add("Retry-After", "4");

            // When
            // Then
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => this._client.GetResponseAsync(request, OneAttempt, CancellationToken.None));
        }

        public void Dispose()
        {
            this._handler.Dispose();
            this._semaphore.Dispose();
            this._httpClient.Dispose();
            this._httpClientFactory.Dispose();
        }

        private static RestRequest CreateRequestFake(string path, string query)
        {
            var request = A.Fake<RestRequest>();
            A.CallTo(() => request.Path).Returns(path);
            A.CallTo(() => request.Query).Returns(query);
            return request;
        }

        private HttpResponseMessage SetupResponseFake(
            HttpStatusCode statusCode,
            string content)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content),
            };
            A.CallTo(() => this._handler.SendAsyncCore(A<HttpRequestMessage>._))
                .Returns(Task.FromResult(response))
                .Once();
            return response;
        }
    }
}
namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Net.Http;

    using Microsoft.Extensions.DependencyInjection;

    public sealed class ApiHttpClientFactory : IApiHttpClientFactory
    {
        private readonly ServiceProvider _provider;

        private readonly IHttpClientFactory _factory;

        public ApiHttpClientFactory()
        {
            var services = new ServiceCollection();
            services.AddHttpClient(
                Microsoft.Extensions.Options.Options.DefaultName,
                client => client.BaseAddress = new Uri("https://api.binance.com/api/v3/"));

            this._provider = services.BuildServiceProvider();
            this._factory = this._provider.GetService<IHttpClientFactory>()
                ?? throw new InvalidOperationException($"Cannot resolve {typeof(IHttpClientFactory)}");
        }

        public HttpClient CreateClient() =>
            this._factory.CreateClient(Microsoft.Extensions.Options.Options.DefaultName);

        public void Dispose() => ((IDisposable)this._provider).Dispose();
    }
}
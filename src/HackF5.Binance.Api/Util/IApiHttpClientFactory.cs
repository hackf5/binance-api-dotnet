namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Net.Http;

    public interface IApiHttpClientFactory : IDisposable
    {
        HttpClient CreateClient();
    }
}
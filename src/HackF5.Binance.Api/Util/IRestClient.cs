namespace HackF5.Binance.Api.Util
{
    using System.Threading;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Request.Rest;

    public interface IRestClient
    {
        Task<string> GetRequestAsync(RestRequest request, int maxAttempts = 10, CancellationToken cancellation = default);
    }
}
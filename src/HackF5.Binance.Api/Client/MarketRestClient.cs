namespace HackF5.Binance.Api.Client
{
    using System.Threading;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Request.Rest.Market;
    using HackF5.Binance.Api.Response.Rest.Market;
    using HackF5.Binance.Api.Util;

    public class MarketRestClient
    {
        private readonly IRestClient _rest;

        public MarketRestClient(IRestClient rest) => this._rest = rest;

        public async Task<OrderBookRestResponse> GetOrderBookAsync(
            OrderBookRestRequest request,
            CancellationToken cancellation = default) => new(
                request,
                await this._rest.GetRequestAsync(request, cancellation: cancellation));

        public async Task<KlineRestResponse> GetKlineAsync(
            KlineRestRequest request,
            CancellationToken cancellation = default) => new(
                request,
                await this._rest.GetRequestAsync(request, cancellation: cancellation));
    }
}
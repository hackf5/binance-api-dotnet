namespace HackF5.Binance.Api.Client
{
    using System.Threading;

    using HackF5.Binance.Api.Request.Stream.Market;
    using HackF5.Binance.Api.Response.Stream.Market;
    using HackF5.Binance.Api.Util;

    public class MarketStreamClient
    {
        private readonly IWebSocketClient _socket;

        public MarketStreamClient(IWebSocketClient socket) => this._socket = socket;

        public KlineStreamResponse GetKlineAsync(
            KlineStreamRequest request,
            CancellationToken cancellation = default) => new(
                request,
                this._socket.GetStreamAsync(request.Path, cancellation),
                cancellation);
    }
}
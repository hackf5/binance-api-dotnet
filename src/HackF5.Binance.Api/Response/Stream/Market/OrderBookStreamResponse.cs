#pragma warning disable CA1711

namespace HackF5.Binance.Api.Response.Stream.Market
{
    using System.Collections.Generic;
    using System.Threading;

    using HackF5.Binance.Api.Model.Stream;
    using HackF5.Binance.Api.Request.Stream.Market;

    public class OrderBookStreamResponse
        : StreamResponse<OrderBookStreamRequest, OrderBookStreamEvent>
    {
        public OrderBookStreamResponse(
            OrderBookStreamRequest request,
            IAsyncEnumerable<string> stream,
            CancellationToken cancellationToken = default)
            : base(request, stream, cancellationToken)
        {
        }
    }
}
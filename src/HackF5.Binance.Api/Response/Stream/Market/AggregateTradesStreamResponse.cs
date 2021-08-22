namespace HackF5.Binance.Api.Response.Stream.Market
{
    using System.Collections.Generic;
    using System.Threading;

    using HackF5.Binance.Api.Model.Stream;
    using HackF5.Binance.Api.Request.Stream.Market;

    public sealed class AggregateTradesStreamResponse
        : StreamResponse<AggregateTradesStreamRequest, AggregateTradeStreamEvent>
    {
        public AggregateTradesStreamResponse(
            AggregateTradesStreamRequest request,
            IAsyncEnumerable<string> stream,
            CancellationToken cancellationToken = default)
            : base(request, stream, cancellationToken)
        {
        }
    }
}
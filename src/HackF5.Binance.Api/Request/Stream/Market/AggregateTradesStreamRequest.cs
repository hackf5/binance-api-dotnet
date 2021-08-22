namespace HackF5.Binance.Api.Request.Stream.Market
{
    public class AggregateTradesStreamRequest : SymbolStreamRequest
    {
        public AggregateTradesStreamRequest(string symbol)
            : base(symbol)
        {
        }

        public override string Parameters => "aggTrade";
    }
}
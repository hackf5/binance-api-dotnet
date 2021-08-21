namespace HackF5.Binance.Api.Request.Stream.Market
{
    public class BookTickerStreamRequest : SymbolStreamRequest
    {
        public BookTickerStreamRequest(string symbol)
            : base(symbol)
        {
        }

        public override string Parameters => "bookTicker";
    }
}
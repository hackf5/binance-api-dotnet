namespace HackF5.Binance.Api.Request.Stream.Market
{
    public class OrderBookStreamRequest : SymbolStreamRequest
    {
        public OrderBookStreamRequest(string symbol, bool highSpeed = false)
            : base(symbol) => this.HighSpeed = highSpeed;

        public bool HighSpeed { get; }

        public override string Parameters => $"depth{(this.HighSpeed ? "@100ms" : string.Empty)}";
    }
}
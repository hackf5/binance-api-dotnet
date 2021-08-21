namespace HackF5.Binance.Api.Request.Stream.Market
{
    public abstract class SymbolStreamRequest : StreamRequest
    {
        protected SymbolStreamRequest(string symbol)
        {
#pragma warning disable CA1308
            // the symbol must be lower case.
            this.Symbol = symbol.ToLowerInvariant();
#pragma warning restore CA1308
        }

        public string Symbol { get; }

        public sealed override string Path => $"{this.Symbol}@{this.Parameters}";
    }
}
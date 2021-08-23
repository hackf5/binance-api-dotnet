namespace HackF5.Binance.Api.Request.Rest.Market
{
    using HackF5.Binance.Api.Request.Rest.Core;

    public abstract class SymbolRestRequest : RestRequest
    {
        protected SymbolRestRequest(string symbol)
        {
            this.Symbol = symbol.ToUpperInvariant();
        }

        [QueryParameter("symbol")]
        public string Symbol { get; }
    }
}
namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System;

    using HackF5.Binance.Api.Request.Rest.Core;

    public abstract class SymbolRestRequest : RestRequest
    {
        protected SymbolRestRequest(string symbol)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                throw new ArgumentException(
                    $"'{nameof(symbol)}' cannot be null or whitespace.", nameof(symbol));
            }

            this.Symbol = symbol.ToUpperInvariant();
        }

        [QueryParameter("symbol")]
        public string Symbol { get; }
    }
}
namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System;

    using HackF5.Binance.Api.Request.Rest.Core;

    public abstract class LimitRestRequest : SymbolRestRequest
    {
        protected LimitRestRequest(string symbol, int limit, Action<int>? validateLimit = null)
            : base(symbol)
        {
            validateLimit?.Invoke(limit);
            this.Limit = limit;
        }

        [QueryParameter("limit")]
        public int Limit { get; }
    }
}
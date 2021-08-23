namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System.Collections.Generic;

    using HackF5.Binance.Api.Request.Rest.Core;

    public class OrderBookRestRequest : LimitRestRequest
    {
        private static readonly HashSet<int> ValidLimits = new(new[] { 5, 10, 20, 50, 100, 500, 1000, 5000 });

        public OrderBookRestRequest(string symbol, int limit = 100)
            : base(
                symbol,
                limit,
                l => LimitValidation.ValidateCollection(l, ValidLimits))
        {
        }

        public override string Path => "depth";

        public override int Weight => this.Limit switch
        {
            <= 100 => 1,
            <= 500 => 5,
            <= 1000 => 10,
            _ => 50,
        };
    }
}
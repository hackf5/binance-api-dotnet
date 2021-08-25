namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Request.Rest.Core;

    public class KlineRestRequest : RangeRestRequest
    {
        public KlineRestRequest(
            string symbol,
            KlineInterval interval = KlineInterval.Minutes1,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int limit = 500)
            : base(
                symbol,
                limit,
                null,
                startTime,
                endTime,
                l => LimitValidation.ValidateRange(l, 1, 1000),
                (s, e) => TimeValidation.ValidateRange(s, e, null))
        {
            this.Interval = interval;
        }

        [QueryParameter("interval")]
        public KlineInterval Interval { get; }

        public override string Path => "klines";

        public override int Weight => 1;
    }
}
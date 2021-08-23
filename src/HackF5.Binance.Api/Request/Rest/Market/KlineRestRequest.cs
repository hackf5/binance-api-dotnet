namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System;

    using EnumsNET;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Request.Rest.Core;

    public class KlineRestRequest : RangeRestRequest
    {
        public KlineRestRequest(
            string symbol,
            KlineInterval interval,
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
                null)
        {
            this.Interval = interval.AsString(EnumFormat.EnumMemberValue)
                ?? throw new ArgumentException($"Unknown interval {interval}.", nameof(interval));
        }

        [QueryParameter("interval")]
        public string Interval { get; }

        public override string Path => "klines";

        public override int Weight => 1;
    }
}
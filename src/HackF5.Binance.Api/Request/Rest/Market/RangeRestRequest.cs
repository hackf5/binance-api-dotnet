namespace HackF5.Binance.Api.Request.Rest.Market
{
    using System;

    using HackF5.Binance.Api.Request.Rest.Core;

    public abstract class RangeRestRequest : LimitRestRequest
    {
        protected RangeRestRequest(
            string symbol,
            int limit,
            long? fromId,
            DateTime? startTime,
            DateTime? endTime,
            Action<int>? validateLimit,
            Action<DateTime?, DateTime?>? validateTime)
            : base(symbol, limit, validateLimit)
        {
            validateTime?.Invoke(startTime, endTime);

            this.FromId = fromId;

            this.StartTime = startTime.HasValue
                ? new DateTimeOffset(startTime.Value).ToUnixTimeMilliseconds()
                : default(long?);

            this.EndTime = endTime.HasValue
                ? new DateTimeOffset(endTime.Value).ToUnixTimeMilliseconds()
                : default(long?);
        }

        [QueryParameter("endTime")]
        public long? EndTime { get; }

        [QueryParameter("fromId")]
        public long? FromId { get; }

        [QueryParameter("startTime")]
        public long? StartTime { get; }
    }
}
namespace HackF5.Binance.Api.Request.Rest.Core
{
    using System;

    public static class TimeValidation
    {
        public static void ValidateRange(DateTime? startTime, DateTime? endTime, TimeSpan? timeSpan)
        {
            if (startTime == default || endTime == default)
            {
                return;
            }

            if (startTime >= endTime)
            {
                throw new ArgumentException(
                    $"Start time {startTime} must be strictly less than End time {endTime}.",
                    nameof(startTime));
            }

            if (timeSpan is null)
            {
                return;
            }

            var interval = endTime - startTime;
            if (interval >= timeSpan)
            {
                throw new ArgumentException(
                    $"The time interval ({interval}) must be strictly less than {timeSpan}.",
                    nameof(endTime));
            }
        }
    }
}
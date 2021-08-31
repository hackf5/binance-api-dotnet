namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class TemporalServices : ITemporalServices
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public Task Delay(TimeSpan delay, CancellationToken cancellation) => Task.Delay(delay, cancellation);
    }
}
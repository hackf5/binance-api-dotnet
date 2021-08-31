namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITemporalServices
    {
        DateTime UtcNow { get; }

        Task Delay(TimeSpan delay, CancellationToken cancellation = default);
    }
}
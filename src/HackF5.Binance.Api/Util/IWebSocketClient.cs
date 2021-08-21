namespace HackF5.Binance.Api.Util
{
    using System.Collections.Generic;
    using System.Threading;

    public interface IWebSocketClient
    {
        IAsyncEnumerable<string> GetStreamAsync(
            string streamName,
            CancellationToken cancellation = default);
    }
}
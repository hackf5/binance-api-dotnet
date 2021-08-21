namespace HackF5.Binance.Api.Tests
{
    using System;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Util;

    using Xunit;
    using Xunit.Abstractions;

    public class Spike
    {
        private readonly ITestOutputHelper _output;

        public Spike(ITestOutputHelper output)
        {
            this._output = output;
        }

        public async Task Spike1Async()
        {
            using var client = new WebSocketClient();

            await client.ConnectAsync(new[] { "btcusdt@aggTrade", "btcusdt@bookTicker" });

            var count = 100;
            await foreach (var item in client.GetStreamAsync())
            {
                this._output.WriteLine(item);

                if (--count == 0)
                {
                    break;
                }
            }
        }
    }
}
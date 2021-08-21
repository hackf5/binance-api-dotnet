namespace HackF5.Binance.Api.Tests
{
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Client;
    using HackF5.Binance.Api.Model.Core;
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
            var client = new WebSocketClient();

            var count = 10;
            await foreach (var item in client.GetStreamAsync("btcusdt@aggTrade"))
            {
                this._output.WriteLine(item);

                if (--count == 0)
                {
                    break;
                }
            }
        }

        public async Task KlineSpikeAsync()
        {
            var client = new MarketStreamClient(new WebSocketClient());

            var count = 10;
            await foreach (var item in client.GetKlineAsync(new("btcusdt", KlineInterval.Minutes1)))
            {
                this._output.WriteLine($"{item!.Payload!.Data!.ClosePrice}");

                if (--count == 0)
                {
                    break;
                }
            }
        }
    }
}
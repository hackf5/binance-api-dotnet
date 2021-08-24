#pragma warning disable xUnit1004

namespace HackF5.Binance.Api.Tests
{
    using System.Linq;
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

            var count = 3;
            await foreach (var item in client.GetKlineAsync(new("btcusdt", KlineInterval.Minutes1)))
            {
                this._output.WriteLine($"{item!.Payload!.Data!.ClosePrice}");

                if (--count == 0)
                {
                    break;
                }
            }
        }

        public async Task BookTickerSpikeAsync()
        {
            var client = new MarketStreamClient(new WebSocketClient());

            var count = 3;
            await foreach (var item in client.GetBookTickerAsync(new("btcusdt")))
            {
                this._output.WriteLine($"{item!.Payload!.BestAskPrice}");

                if (--count == 0)
                {
                    break;
                }
            }
        }

        public async Task OrderBookSpikeAsync()
        {
            var client = new MarketStreamClient(new WebSocketClient());

            var count = 3;
            await foreach (var item in client.GetOrderBookAsync(new("btcusdt")))
            {
                this._output.WriteLine($"{item!.Payload!.Asks.Length}");

                if (--count == 0)
                {
                    break;
                }
            }
        }

        public async Task AggregateTradesSpikeAsync()
        {
            var client = new MarketStreamClient(new WebSocketClient());

            var count = 3;
            await foreach (var item in client.GetAggregateTradesAsync(new("btcusdt")))
            {
                this._output.WriteLine($"{item!.Payload!.Price}");

                if (--count == 0)
                {
                    break;
                }
            }
        }

        public async Task KlineRestSpikeAsync()
        {
            using var factory = new ApiHttpClientFactory();
            using var semaphore = new RequestSemaphore();

            var rest = new RestClient(factory, semaphore);
            var client = new MarketRestClient(rest);

            var response = await client.GetKlineAsync(
                new("btcusdt", KlineInterval.Minutes3));

            this._output.WriteLine($"{response.Payload.Length}");
        }

        public async Task OrderBookRestSpikeAsync()
        {
            using var factory = new ApiHttpClientFactory();
            using var semaphore = new RequestSemaphore();

            var rest = new RestClient(factory, semaphore);
            var client = new MarketRestClient(rest);

            var response = await client.GetOrderBookAsync(new("btcusdt"));

            this._output.WriteLine($"{response.Payload.Asks.Length}");
        }
    }
}
namespace HackF5.Binance.Api.Tests.Client
{
    using System.Threading.Tasks;

    using FakeItEasy;

    using HackF5.Binance.Api.Client;
    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Model.Rest.Market;
    using HackF5.Binance.Api.Request.Rest.Market;
    using HackF5.Binance.Api.Tests.Comparison;
    using HackF5.Binance.Api.Util;

    using Xunit;

    public class MarketRestClientTests
    {
        [Fact]
        public async Task GetOrderBookAsync_ValidRequest_ReturnsResponse()
        {
            // Given
            var rest = A.Fake<IRestClient>().SetupGetResponseAsync("MarketRestClient/orderBook1.json");
            var client = new MarketRestClient(rest);
            var request = new OrderBookRestRequest("btcusdt");

            var expectedPayload = new OrderBookRestData
            {
                LastUpdateId = 1027024,
                Bids = new OrderBookItem[]
                {
                     new() { Price = 4, Quantity = 431 },
                     new() { Price = 7, Quantity = 123.005m },
                },
                Asks = new OrderBookItem[]
                {
                     new() { Price = 4.000002m, Quantity = 12 },
                     new() { Price = 65.00052m, Quantity = 6.00006m },
                },
            };

            // When
            var response = await client.GetOrderBookAsync(request);

            // Then
            Assert.Equal(request, response.Request);
            Assert.Equal(expectedPayload, response.Payload, OrderBookRestDataEqualityComparer.Instance);
        }

        [Fact]
        public async Task GetKlineAsync_ValidRequest_ReturnsResponse()
        {
            // Given
            var rest = A.Fake<IRestClient>().SetupGetResponseAsync("MarketRestClient/kline1.jsonc");
            var client = new MarketRestClient(rest);
            var request = new KlineRestRequest("btcusdt");

            var expectedPayload = new KlineRestData[]
            {
                new()
                {
                    BaseAssetVolume = 148976.11427815m,
                    ClosePrice = 0.015771m,
                    CloseTime = 1499644799999L.FromUnixTime(),
                    HighPrice = 0.8m,
                    LowPrice = 0.015758m,
                    NumberOfTrades = 308,
                    OpenPrice = 0.0163479m,
                    OpenTime = 1499040000000L.FromUnixTime(),
                    QuoteAssetVolume = 2434.19055334m,
                    TakerBuyBaseAssetVolume = 1756.87402397m,
                    TakerBuyQuoteAssetVolume = 28.46694368m,
                },
            };

            // When
            var response = await client.GetKlineAsync(request);

            // Then
            Assert.Equal(request, response.Request);
            Assert.Equal(expectedPayload, response.Payload, KlineRestDataComparer.Instance);
        }
    }
}
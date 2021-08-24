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
            // Arrange
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

            // Act
            var response = await client.GetOrderBookAsync(request);

            // Assert
            Assert.Equal(request, response.Request);
            Assert.Equal(expectedPayload, response.Payload, OrderBookRestDataEqualityComparer.Instance);
        }
    }
}
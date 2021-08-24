namespace HackF5.Binance.Api.Tests.Request.Market
{
    using System;

    using HackF5.Binance.Api.Request.Rest.Market;

    using Xunit;

    public class OrderBookRestRequestTests
    {
        [Theory]
        [InlineData("btcusdt")]
        [InlineData("BTCUSDT")]
        [InlineData("BTcusDT")]
        public void Ctor_Symbol_IsConvertedToUpperCase(string symbol)
        {
            // Given
            // When
            var request = new OrderBookRestRequest(symbol);

            // Then
            Assert.Equal("BTCUSDT", request.Symbol);
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(10, 1)]
        [InlineData(20, 1)]
        [InlineData(50, 1)]
        [InlineData(100, 1)]
        [InlineData(500, 5)]
        [InlineData(1000, 10)]
        [InlineData(5000, 50)]
        public void Ctor_ValidLimit_AssignsLimitAndWeight(int limit, int weight)
        {
            // Given
            // When
            var request = new OrderBookRestRequest("btcusdt", limit);

            // Then
            Assert.Equal(limit, request.Limit);
            Assert.Equal(weight, request.Weight);
        }

        [Fact]
        public void Ctor_DefaultLimit_Is100()
        {
            // Given
            // When
            var request = new OrderBookRestRequest("btcusdt");

            // Then
            Assert.Equal(100, request.Limit);
        }

        [Theory]
        [InlineData("btcusdt", 500)]
        [InlineData("ethusdt", 1000)]
        public void Ctor_ValidParameters_PathIsDepth(string symbol, int limit)
        {
            // Given
            // When
            var request = new OrderBookRestRequest(symbol, limit);

            // Then
            Assert.Equal("depth", request.Path);
        }

        [Theory]
        [InlineData("btcusdt", 500, "limit=500&symbol=BTCUSDT")]
        [InlineData("ethusdt", 1000, "limit=1000&symbol=ETHUSDT")]
        public void Ctor_ValidParameters_Query(string symbol, int limit, string query)
        {
            // Given
            // When
            var request = new OrderBookRestRequest(symbol, limit);

            // Then
            Assert.Equal(query, request.Query);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\t")]
        public void Ctor_NullOrWhitespaceSymbol_Throws(string? symbol)
        {
            // Given
            // When
            // Then
            var ex = Assert.Throws<ArgumentException>(() => new OrderBookRestRequest(symbol!));
            Assert.Equal(nameof(symbol), ex.ParamName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(15)]
        [InlineData(150)]
        [InlineData(940)]
        [InlineData(2500)]
        [InlineData(5001)]
        public void Ctor_InvalidLimit_Throws(int limit)
        {
            // Given
            // When
            // Then
            var ex = Assert.Throws<ArgumentException>(() => new OrderBookRestRequest("btcusdt", limit));
            Assert.Equal(nameof(limit), ex.ParamName);
        }
    }
}
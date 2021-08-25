namespace HackF5.Binance.Api.Tests.Request.Market
{
    using System;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Request.Rest.Market;
    using HackF5.Binance.Api.Tests.Client;

    using Xunit;

    public class KlineRestRequestTests
    {
        [Theory]
        [InlineData("ethusdt")]
        [InlineData("ETHUSDT")]
        [InlineData("EThusDT")]
        public void Ctor_Symbol_IsConvertedToUpperCase(string symbol)
        {
            // Given
            // When
            var request = new KlineRestRequest(symbol);

            // Then
            Assert.Equal("ETHUSDT", request.Symbol);
        }

        [Theory]
        [InlineData(KlineInterval.Days1)]
        [InlineData(KlineInterval.Minutes1)]
        [InlineData(KlineInterval.Weeks1)]
        public void Ctor_Interval_IsAssignedToInterval(KlineInterval interval)
        {
            // Given
            // When
            var request = new KlineRestRequest("ethusdt", interval);

            // Then
            Assert.Equal(interval, request.Interval);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(28)]
        [InlineData(53)]
        [InlineData(177)]
        [InlineData(512)]
        [InlineData(1000)]
        public void Ctor_ValidLimit_AssignsLimitAndWeight(int limit)
        {
            // Given
            // When
            var request = new KlineRestRequest("btcusdt", limit: limit);

            // Then
            Assert.Equal(limit, request.Limit);
            Assert.Equal(1, request.Weight);
        }

        [Theory]
        [InlineData("btcusdt", KlineInterval.Hours1)]
        [InlineData("ethusdt", KlineInterval.Minutes15)]
        [InlineData("ethusdt", KlineInterval.Minutes3)]
        public void Ctor_ValidParameters_PathIsDepth(string symbol, KlineInterval interval)
        {
            // Given
            // When
            var request = new KlineRestRequest(symbol, interval);

            // Then
            Assert.Equal("klines", request.Path);
        }

        [Theory]
        [InlineData("btcusdt", 500, KlineInterval.Hours1, 100, 200, "endTime=200&interval=1h&limit=500&startTime=100&symbol=BTCUSDT")]
        [InlineData("ethusdt", 1000, KlineInterval.Minutes3, 2000, null, "interval=3m&limit=1000&startTime=2000&symbol=ETHUSDT")]
        [InlineData("ethusdt", 124, KlineInterval.Days1, null, 9999, "endTime=9999&interval=1d&limit=124&symbol=ETHUSDT")]
        [InlineData("btcusdt", 888, KlineInterval.Months1, null, null, "interval=1M&limit=888&symbol=BTCUSDT")]
        public void Ctor_ValidParameters_Query(
            string symbol, int limit, KlineInterval interval, long? start, long? end, string query)
        {
            // Given
            // When
            var request = new KlineRestRequest(symbol, interval, start.FromUnixTime(), end.FromUnixTime(), limit);

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
            var ex = Assert.Throws<ArgumentException>(() => new KlineRestRequest(symbol!));
            Assert.Equal(nameof(symbol), ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5001)]
        public void Ctor_InvalidLimit_Throws(int limit)
        {
            // Given
            // When
            // Then
            var ex = Assert.Throws<ArgumentException>(() => new KlineRestRequest("btcusdt", limit: limit));
            Assert.Equal(nameof(limit), ex.ParamName);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        public void Ctor_StartAndEndAreOutOfOrder_Throws(long? startTime, long? endTime)
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new KlineRestRequest(
                    "btcusdt", startTime: startTime.FromUnixTime(), endTime: endTime.FromUnixTime()));
            Assert.Equal(nameof(startTime), ex.ParamName);
        }
    }
}
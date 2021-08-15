namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class TickerStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public TickerStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("p")] decimal priceChange,
            [JsonProperty("P")] decimal priceChangePercent,
            [JsonProperty("w")] decimal weightedAveragePrice,
            [JsonProperty("x")] decimal previousPrice,
            [JsonProperty("c")] decimal closePrice,
            [JsonProperty("Q")] decimal closeQuantity,
            [JsonProperty("b")] decimal bestBidPrice,
            [JsonProperty("B")] decimal bestBidQuantity,
            [JsonProperty("a")] decimal bestAskPrice,
            [JsonProperty("A")] decimal bestAskQuantity,
            [JsonProperty("o")] decimal openPrice,
            [JsonProperty("h")] decimal highPrice,
            [JsonProperty("l")] decimal lowPrice,
            [JsonProperty("v")] decimal totalTradedBaseAssetVolume,
            [JsonProperty("q")] decimal totalTradedQuoteAssetVolume,
            [JsonProperty("O")] long statisticsOpenTime,
            [JsonProperty("C")] long statisticsCloseTime,
            [JsonProperty("F")] long firstTradeId,
            [JsonProperty("L")] long lastTradeId,
            [JsonProperty("n")] long totalNumberOfTrades)
            : base(eventType, eventTime, symbol)
        {
            this.PriceChange = priceChange;
            this.PriceChangePercent = priceChangePercent;
            this.WeightedAveragePrice = weightedAveragePrice;
            this.PreviousPrice = previousPrice;
            this.ClosePrice = closePrice;
            this.CloseQuantity = closeQuantity;
            this.BestBidPrice = bestBidPrice;
            this.BestBidQuantity = bestBidQuantity;
            this.BestAskPrice = bestAskPrice;
            this.BestAskQuantity = bestAskQuantity;
            this.OpenPrice = openPrice;
            this.HighPrice = highPrice;
            this.LowPrice = lowPrice;
            this.TotalTradedBaseAssetVolume = totalTradedBaseAssetVolume;
            this.TotalTradedQuoteAssetVolume = totalTradedQuoteAssetVolume;
            this.StatisticsOpenTime = statisticsOpenTime;
            this.StatisticsCloseTime = statisticsCloseTime;
            this.FirstTradeId = firstTradeId;
            this.LastTradeId = lastTradeId;
            this.TotalNumberOfTrades = totalNumberOfTrades;
        }

        public decimal BestAskPrice { get; }

        public decimal BestAskQuantity { get; }

        public decimal BestBidPrice { get; }

        public decimal BestBidQuantity { get; }

        public long FirstTradeId { get; }

        public decimal HighPrice { get; }

        public decimal ClosePrice { get; }

        public decimal CloseQuantity { get; }

        public long LastTradeId { get; }

        public decimal LowPrice { get; }

        public decimal OpenPrice { get; }

        public decimal PreviousPrice { get; }

        public decimal PriceChange { get; }

        public decimal PriceChangePercent { get; }

        public long StatisticsCloseTime { get; }

        public long StatisticsOpenTime { get; }

        public long TotalNumberOfTrades { get; }

        public decimal TotalTradedBaseAssetVolume { get; }

        public decimal TotalTradedQuoteAssetVolume { get; }

        public decimal WeightedAveragePrice { get; }
    }
}
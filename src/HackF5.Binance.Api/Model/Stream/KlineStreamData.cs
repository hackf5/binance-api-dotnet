namespace HackF5.Binance.Api.Model.Stream
{
    using System;
    using System.Diagnostics;

    using EnumsNET;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [DebuggerDisplay("T:{OpenTime} - {CloseTime}, o:{OpenPrice}, c:{ClosePrice}, h:{HighPrice}, l:{LowPrice}")]
    public class KlineStreamData : IKlineItem
    {
        [JsonConstructor]
        public KlineStreamData(
            [JsonProperty("t"), JsonConverter(typeof(UnixTimeConverter))] DateTime openTime,
            [JsonProperty("T"), JsonConverter(typeof(UnixTimeConverter))] DateTime closeTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("i"), JsonConverter(typeof(StringEnumConverter))] KlineInterval interval,
            [JsonProperty("f")] long firstTradeId,
            [JsonProperty("L")] long lastTradeId,
            [JsonProperty("o")] decimal openPrice,
            [JsonProperty("c")] decimal closePrice,
            [JsonProperty("h")] decimal highPrice,
            [JsonProperty("l")] decimal lowPrice,
            [JsonProperty("v")] decimal baseAssetVolume,
            [JsonProperty("n")] long numberOfTrades,
            [JsonProperty("x")] bool isClosed,
            [JsonProperty("q")] decimal quoteAssetVolume,
            [JsonProperty("V")] decimal takerBuyBaseAssetVolume,
            [JsonProperty("Q")] decimal takerBuyQuoteAssetVolume)
        {
            this.Symbol = symbol;
            this.Interval = interval;
            this.FirstTradeId = firstTradeId;
            this.LastTradeId = lastTradeId;
            this.OpenPrice = openPrice;
            this.ClosePrice = closePrice;
            this.HighPrice = highPrice;
            this.LowPrice = lowPrice;
            this.BaseAssetVolume = baseAssetVolume;
            this.NumberOfTrades = numberOfTrades;
            this.IsClosed = isClosed;
            this.QuoteAssetVolume = quoteAssetVolume;
            this.TakerBuyBaseAssetVolume = takerBuyBaseAssetVolume;
            this.TakerBuyQuoteAssetVolume = takerBuyQuoteAssetVolume;
            this.OpenTime = openTime;
            this.CloseTime = closeTime;
        }

        public decimal BaseAssetVolume { get; }

        public decimal ClosePrice { get; }

        public DateTime CloseTime { get; }

        public long FirstTradeId { get; }

        public decimal HighPrice { get; }

        public KlineInterval Interval { get; }

        public bool IsClosed { get; }

        public long LastTradeId { get; }

        public decimal LowPrice { get; }

        public long NumberOfTrades { get; }

        public decimal OpenPrice { get; }

        public DateTime OpenTime { get; }

        public decimal QuoteAssetVolume { get; }

        public string Symbol { get; }

        public decimal TakerBuyBaseAssetVolume { get; }

        public decimal TakerBuyQuoteAssetVolume { get; }
    }
}
namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class MiniTickerStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public MiniTickerStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("c")] string closePrice,
            [JsonProperty("o")] string openPrice,
            [JsonProperty("h")] string highPrice,
            [JsonProperty("l")] string lowPrice,
            [JsonProperty("v")] string totalTradedBaseAssetVolume,
            [JsonProperty("q")] string totalTradedQuoteAssetVolume)
            : base(eventType, eventTime, symbol)
        {
            this.ClosePrice = closePrice;
            this.OpenPrice = openPrice;
            this.HighPrice = highPrice;
            this.LowPrice = lowPrice;
            this.TotalTradedBaseAssetVolume = totalTradedBaseAssetVolume;
            this.TotalTradedQuoteAssetVolume = totalTradedQuoteAssetVolume;
        }

        public string ClosePrice { get; }

        public DateTime EndTime => this.EventTime;

        public string HighPrice { get; }

        public string LowPrice { get; }

        public string OpenPrice { get; }

        public DateTime StartTime => this.EventTime.AddDays(-1);

        public string TotalTradedBaseAssetVolume { get; }

        public string TotalTradedQuoteAssetVolume { get; }
    }
}
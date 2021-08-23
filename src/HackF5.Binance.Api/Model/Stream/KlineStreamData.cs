namespace HackF5.Binance.Api.Model.Stream
{
    using System;
    using System.Diagnostics;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Model.Core.Util;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [DebuggerDisplay("T:{OpenTime} - {CloseTime}, o:{OpenPrice}, c:{ClosePrice}, h:{HighPrice}, l:{LowPrice}")]
    public class KlineStreamData
    {
        [JsonProperty("v")]
        public decimal BaseAssetVolume { get; set; }

        [JsonProperty("c")]
        public decimal ClosePrice { get; set; }

        [JsonProperty("T")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime CloseTime { get; set; }

        [JsonProperty("f")]
        public long FirstTradeId { get; set; }

        [JsonProperty("h")]
        public decimal HighPrice { get; set; }

        [JsonProperty("i")]
        [JsonConverter(typeof(StringEnumConverter))]
        public KlineInterval Interval { get; set; }

        [JsonProperty("x")]
        public bool IsClosed { get; set; }

        [JsonProperty("L")]
        public long LastTradeId { get; set; }

        [JsonProperty("l")]
        public decimal LowPrice { get; set; }

        [JsonProperty("n")]
        public long NumberOfTrades { get; set; }

        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }

        [JsonProperty("t")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime OpenTime { get; set; }

        [JsonProperty("q")]
        public decimal QuoteAssetVolume { get; set; }

        [JsonProperty("V")]
        public decimal TakerBuyBaseAssetVolume { get; set; }

        [JsonProperty("Q")]
        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
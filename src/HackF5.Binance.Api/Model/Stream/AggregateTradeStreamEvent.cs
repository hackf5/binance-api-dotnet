namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class AggregateTradeStreamEvent : SymbolStreamEvent
    {
        [JsonProperty("a")]
        public long AggregateTradeId { get; set; }

        [JsonProperty("f")]
        public long FirstTradeId { get; set; }

        [JsonProperty("m")]
        public bool IsBuyerMaker { get; set; }

        [JsonProperty("l")]
        public long LastTradeId { get; set; }

        [JsonProperty("p")]
        public decimal Price { get; set; }

        [JsonProperty("q")]
        public decimal Quantity { get; set; }

        [JsonProperty("T")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime TradeTime { get; set; }
    }
}
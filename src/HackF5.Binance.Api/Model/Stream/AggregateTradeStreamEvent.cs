namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class AggregateTradeStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public AggregateTradeStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("a")] long aggregateTradeId,
            [JsonProperty("f")] long firstTradeId,
            [JsonProperty("m")] bool isBuyerMaker,
            [JsonProperty("l")] long lastTradeId,
            [JsonProperty("p")] decimal price,
            [JsonProperty("q")] decimal quantity,
            [JsonProperty("T"), JsonConverter(typeof(UnixTimeConverter))] DateTime tradeTime)
        : base(eventType, eventTime, symbol)
        {
            this.AggregateTradeId = aggregateTradeId;
            this.FirstTradeId = firstTradeId;
            this.IsBuyerMaker = isBuyerMaker;
            this.LastTradeId = lastTradeId;
            this.Price = price;
            this.Quantity = quantity;
            this.TradeTime = tradeTime;
        }

        public DateTime TradeTime { get; set; }

        public long AggregateTradeId { get; }

        public long FirstTradeId { get; }

        public bool IsBuyerMaker { get; }

        public long LastTradeId { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }
    }
}
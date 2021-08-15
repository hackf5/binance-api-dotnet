namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class TradeStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public TradeStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("t")] long tradeId,
            [JsonProperty("p")] decimal price,
            [JsonProperty("q")] decimal quantity,
            [JsonProperty("b")] long buyerId,
            [JsonProperty("a")] long sellerId,
            [JsonProperty("m")] bool isBuyerMaker,
            [JsonProperty("T"), JsonConverter(typeof(UnixTimeConverter))] DateTime tradeTime)
            : base(eventType, eventTime, symbol)
        {
            this.TradeId = tradeId;
            this.Price = price;
            this.Quantity = quantity;
            this.BuyerId = buyerId;
            this.SellerId = sellerId;
            this.IsBuyerMaker = isBuyerMaker;
            this.TradeTime = tradeTime;
        }

        public long BuyerId { get; }

        public bool IsBestMatch { get; }

        public bool IsBuyerMaker { get; }

        public decimal Price { get; }

        public decimal Quantity { get; }

        public long SellerId { get; }

        public long TradeId { get; }

        public DateTime TradeTime { get; }
    }
}
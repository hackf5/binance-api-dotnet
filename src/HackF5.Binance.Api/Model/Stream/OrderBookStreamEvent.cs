namespace HackF5.Binance.Api.Model.Stream
{
    using System;
    using System.Collections.Generic;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class OrderBookStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public OrderBookStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("U")] long firstUpdateId,
            [JsonProperty("u")] long lastUpdateId,
            [JsonProperty("b")] IReadOnlyList<OrderBookItem> bids,
            [JsonProperty("a")] IReadOnlyList<OrderBookItem> asks)
            : base(eventType, eventTime, symbol)
        {
            this.FirstUpdateId = firstUpdateId;
            this.LastUpdateId = lastUpdateId;
            this.Bids = bids;
            this.Asks = asks;
        }

        public IReadOnlyList<OrderBookItem> Asks { get; }

        public IReadOnlyList<OrderBookItem> Bids { get; }

        public long FirstUpdateId { get; }

        public long LastUpdateId { get; }
    }
}
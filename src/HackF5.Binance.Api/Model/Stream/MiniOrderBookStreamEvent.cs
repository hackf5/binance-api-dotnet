namespace HackF5.Binance.Api.Model.Stream
{
    using System;
    using System.Collections.Generic;

    using HackF5.Binance.Api.Model.Core;

    using Newtonsoft.Json;

    public class MiniOrderBookStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public MiniOrderBookStreamEvent(
            long lastUpdateId,
            IReadOnlyList<OrderBookItem> bids,
            IReadOnlyList<OrderBookItem> asks)
            : base("miniBookDepth", DateTime.UtcNow, "{see request}")
        {
            this.LastUpdateId = lastUpdateId;
            this.Bids = bids;
            this.Asks = asks;
        }

        public IReadOnlyList<OrderBookItem> Asks { get; }

        public IReadOnlyList<OrderBookItem> Bids { get; }

        public long LastUpdateId { get; }
    }
}
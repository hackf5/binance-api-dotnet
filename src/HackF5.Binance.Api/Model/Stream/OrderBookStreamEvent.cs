namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class OrderBookStreamEvent : SymbolStreamEvent
    {
        [JsonProperty("U")]
        public long FirstUpdateId { get; set; }

        [JsonProperty("u")]
        public long LastUpdateId { get; set; }

#pragma warning disable CA1819
        [JsonProperty("b")]
        [JsonConverter(typeof(OrderBookItemArrayConverter))]
        public OrderBookItem[] Bids { get; set; } = Array.Empty<OrderBookItem>();

        [JsonProperty("a")]
        [JsonConverter(typeof(OrderBookItemArrayConverter))]
        public OrderBookItem[] Asks { get; set; } = Array.Empty<OrderBookItem>();
#pragma warning restore CA1819
    }
}
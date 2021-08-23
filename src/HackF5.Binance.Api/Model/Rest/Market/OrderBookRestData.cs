namespace HackF5.Binance.Api.Model.Rest.Market
{
    using System;

    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Model.Core.Util;

    using Newtonsoft.Json;

    public class OrderBookRestData
    {
        [JsonProperty("lastUpdateId")]
        public long LastUpdateId { get; set; }

#pragma warning disable CA1819
        [JsonProperty("bids")]
        [JsonConverter(typeof(OrderBookItemArrayConverter))]
        public OrderBookItem[] Bids { get; set; } = Array.Empty<OrderBookItem>();

        [JsonProperty("asks")]
        [JsonConverter(typeof(OrderBookItemArrayConverter))]
        public OrderBookItem[] Asks { get; set; } = Array.Empty<OrderBookItem>();
#pragma warning restore CA1819
    }
}
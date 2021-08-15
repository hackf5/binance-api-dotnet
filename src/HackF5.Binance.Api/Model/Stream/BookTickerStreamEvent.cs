namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using Newtonsoft.Json;

    public class BookTickerStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public BookTickerStreamEvent(
            [JsonProperty("s")] string symbol,
            [JsonProperty("u")] long orderBookUpdateId,
            [JsonProperty("b")] decimal bestBidPrice,
            [JsonProperty("B")] decimal bestBidQuantity,
            [JsonProperty("a")] decimal bestAskPrice,
            [JsonProperty("A")] decimal bestAskQuantity)
            : base("bookTicker", DateTime.UtcNow, symbol)
        {
            this.OrderBookUpdateId = orderBookUpdateId;
            this.BestBidPrice = bestBidPrice;
            this.BestBidQuantity = bestBidQuantity;
            this.BestAskPrice = bestAskPrice;
            this.BestAskQuantity = bestAskQuantity;
        }

        public decimal BestAskPrice { get; }

        public decimal BestAskQuantity { get; }

        public decimal BestBidPrice { get; }

        public decimal BestBidQuantity { get; }

        public long OrderBookUpdateId { get; }
    }
}
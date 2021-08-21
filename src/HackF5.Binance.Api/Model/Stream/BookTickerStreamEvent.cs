namespace HackF5.Binance.Api.Model.Stream
{
    using Newtonsoft.Json;

    public class BookTickerStreamEvent : SymbolStreamEvent
    {
        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }

        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }

        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }

        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }

        [JsonProperty("u")]
        public long OrderBookUpdateId { get; set; }
    }
}
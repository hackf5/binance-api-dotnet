namespace HackF5.Binance.Api.Model.Core
{
    using System.Diagnostics;

    using Newtonsoft.Json;

    [DebuggerDisplay("Price={Price}, Quantity={Quantity}")]
    public class OrderBookItem
    {
        [JsonProperty(Order = 0)]
        public decimal Price { get; set; }

        [JsonProperty(Order = 1)]
        public decimal Quantity { get; set; }
    }
}
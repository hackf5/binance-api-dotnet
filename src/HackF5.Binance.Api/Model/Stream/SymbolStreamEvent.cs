namespace HackF5.Binance.Api.Model.Stream
{
    using Newtonsoft.Json;

    public class SymbolStreamEvent : StreamEvent
    {
        [JsonProperty("s")]
        public string Symbol { get; set; } = string.Empty;
    }
}
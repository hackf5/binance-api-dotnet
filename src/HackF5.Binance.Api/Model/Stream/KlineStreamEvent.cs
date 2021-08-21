namespace HackF5.Binance.Api.Model.Stream
{
    using Newtonsoft.Json;

    public class KlineStreamEvent : SymbolStreamEvent
    {
        [JsonProperty(PropertyName = "K")]
        public KlineStreamData? Data { get; set; }
    }
}
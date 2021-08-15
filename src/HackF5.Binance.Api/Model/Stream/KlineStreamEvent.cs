namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class KlineStreamEvent : StreamEvent
    {
        [JsonConstructor]
        public KlineStreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol,
            [JsonProperty("k")] KlineStreamData data)
            : base(eventType, eventTime, symbol)
        {
            this.Data = data;
        }

        public KlineStreamData Data { get; }
    }
}
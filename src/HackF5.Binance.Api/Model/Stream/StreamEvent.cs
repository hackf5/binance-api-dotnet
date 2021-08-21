namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using EnumsNET;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class StreamEvent
    {
        [JsonProperty("e")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StreamEventType EventType { get; set; }

        [JsonProperty("s")]
        public string? Symbol { get; set; }

        [JsonProperty("E")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime EventTime { get; }
    }
}
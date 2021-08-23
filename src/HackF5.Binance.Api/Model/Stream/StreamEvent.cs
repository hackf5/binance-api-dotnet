namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using HackF5.Binance.Api.Model.Core.Util;

    using Newtonsoft.Json;

    public class StreamEvent
    {
        [JsonProperty("E")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime EventTime { get; }
    }
}
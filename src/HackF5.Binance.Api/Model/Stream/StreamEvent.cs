namespace HackF5.Binance.Api.Model.Stream
{
    using System;

    using EnumsNET;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    public class StreamEvent
    {
        [JsonConstructor]
        public StreamEvent(
            [JsonProperty("e")] string eventType,
            [JsonProperty("E"), JsonConverter(typeof(UnixTimeConverter))] DateTime eventTime,
            [JsonProperty("s")] string symbol)
        {
            this.EventType = Enums.Parse<StreamEventType>(eventType, false, EnumFormat.Description);
            this.EventTime = eventTime;
            this.Symbol = symbol;
        }

        public StreamEventType EventType { get; }

        public string Symbol { get; }

        public DateTime EventTime { get; }
    }
}
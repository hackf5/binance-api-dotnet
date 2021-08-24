namespace HackF5.Binance.Api.Model.Core.Util
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;

    public class UnixTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(
            JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            return value == null
                ? DateTime.UnixEpoch
                : DateTimeOffset.FromUnixTimeMilliseconds((long)value).DateTime;
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteRawValue(new DateTimeOffset(value).ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture));
        }
    }
}
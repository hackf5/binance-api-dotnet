namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    using NJsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class UnixTimeConverter : DateTimeConverterBase
    {
        public override object? ReadJson(
            JsonReader reader, Type objectType, object? existingValue, NJsonSerializer serializer)
        {
            var value = reader.Value;
            return value == null
                ? DateTime.UnixEpoch
                : (object)DateTimeOffset.FromUnixTimeMilliseconds((long)value).DateTime;
        }

        public override void WriteJson(JsonWriter writer, object? value, NJsonSerializer serializer)
        {
            var offset = value switch
            {
                DateTime t => new DateTimeOffset(t),
                DateTimeOffset o => o,
                _ => throw new ArgumentException($"Unknown date value {value}."),
            };

            writer.WriteRawValue(offset.ToUnixTimeMilliseconds().ToString(CultureInfo.InvariantCulture));
        }
    }
}
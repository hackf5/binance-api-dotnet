namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class UnixTimeConverter : DateTimeConverterBase
    {
        public override bool CanRead => base.CanRead;

        public override bool CanWrite => base.CanWrite;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var value = reader.Value;
            return value == null
                ? DateTime.UnixEpoch
                : (object)DateTimeOffset.FromUnixTimeMilliseconds((long)value).DateTime;
        }

        public override void WriteJson(JsonWriter writer, object? value, Newtonsoft.Json.JsonSerializer serializer)
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
namespace HackF5.Binance.Api.Model.Core.Util
{
    using System;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    using NJsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class StringEnumConverterSlim<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
    {
        public override TEnum ReadJson(
            JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, NJsonSerializer serializer)
        {
            return ((string)reader.Value!).FromEnumMember<TEnum>();
        }

        public override void WriteJson(JsonWriter writer, TEnum value, NJsonSerializer serializer)
        {
            writer.WriteValue(value.AsEnumMember());
        }
    }
}
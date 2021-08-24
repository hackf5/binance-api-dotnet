namespace HackF5.Binance.Api.Model.Core.Util
{
    using System;

    using EnumsNET;

    using Newtonsoft.Json;

    public class StringEnumConverterSlim<TEnum> : JsonConverter<TEnum>
    where TEnum : struct, Enum
    {
        public override TEnum ReadJson(
            JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Enums.Parse<TEnum>((string)reader.Value!, false, EnumFormat.EnumMemberValue);
        }

        public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
        {
            writer.WriteValue(value.AsString(EnumFormat.EnumMemberValue));
        }
    }
}
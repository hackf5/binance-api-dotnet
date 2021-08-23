namespace HackF5.Binance.Api.Model.Rest.Market.Util
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using NJsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class KlineRestDataConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, NJsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(
            JsonReader reader, Type objectType, object? existingValue, NJsonSerializer serializer)
        {
            var data = JArray.Load(reader);
            return new KlineRestData
            {
                OpenTime = AsDateTime(data[0]),
                OpenPrice = (decimal)data[1],
                HighPrice = (decimal)data[2],
                LowPrice = (decimal)data[3],
                ClosePrice = (decimal)data[4],
                BaseAssetVolume = (decimal)data[5],
                CloseTime = AsDateTime(data[6]),
                QuoteAssetVolume = (decimal)data[7],
                NumberOfTrades = (int)data[8],
                TakerBuyBaseAssetVolume = (decimal)data[9],
                TakerBuyQuoteAssetVolume = (decimal)data[10],
            };
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotSupportedException();
        }

        private static DateTime AsDateTime(JToken value) =>
            DateTimeOffset.FromUnixTimeMilliseconds((long)value).UtcDateTime;
    }
}
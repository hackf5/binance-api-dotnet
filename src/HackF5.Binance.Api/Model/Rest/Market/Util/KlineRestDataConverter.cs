namespace HackF5.Binance.Api.Model.Rest.Market.Util
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class KlineRestDataConverter : JsonConverter<KlineRestData?>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, KlineRestData? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override KlineRestData? ReadJson(
            JsonReader reader,
            Type objectType,
            KlineRestData? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
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

        private static DateTime AsDateTime(JToken value) =>
            DateTimeOffset.FromUnixTimeMilliseconds((long)value).UtcDateTime;
    }
}
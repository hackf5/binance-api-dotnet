namespace HackF5.Binance.Api.Util
{
    using System;

    using HackF5.Binance.Api.Model.Core;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using NJsonSerializer = Newtonsoft.Json.JsonSerializer;

    public class OrderBookItemArrayConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, NJsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, NJsonSerializer serializer)
        {
            var items = JArray.Load(reader);
            var result = new OrderBookItem[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                result[i] = new OrderBookItem
                {
                    Price = (decimal)item[0]!,
                    Quantity = (decimal)item[1]!,
                };
            }

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotSupportedException();
        }
    }
}
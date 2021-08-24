namespace HackF5.Binance.Api.Model.Core.Util
{
    using System;

    using HackF5.Binance.Api.Model.Core;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class OrderBookItemArrayConverter : JsonConverter<OrderBookItem[]?>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, OrderBookItem[]? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override OrderBookItem[]? ReadJson(
            JsonReader reader, Type objectType, OrderBookItem[]? existingValue, bool hasExistingValue, JsonSerializer serializer)
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
    }
}
namespace HackF5.Binance.Api.Model.Core
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    [JsonArray]
    [DebuggerDisplay("Price: {Price}, Quantity: {Quantity}")]
    public class OrderBookItem
    {
        [JsonConstructor]
        public OrderBookItem(IEnumerable<object> data)
        {
            IndexMapper.Map(data, this);
        }

        [Index(0)]
        public decimal Price
        {
            get;
            private set;
        }

        [Index(1)]
        public decimal Quantity
        {
            get;
            private set;
        }
    }
}
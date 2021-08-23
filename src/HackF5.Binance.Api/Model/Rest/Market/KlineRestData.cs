namespace Up.Crypto.Binance.Model.Rest
{
    using System;
    using System.Diagnostics;

    using HackF5.Binance.Api.Util;

    using Newtonsoft.Json;

    [JsonConverter(typeof(KlineRestDataConverter))]
    [DebuggerDisplay("T:{OpenTime} - {CloseTime}, o:{OpenPrice}, c:{ClosePrice}, h:{HighPrice}, l:{LowPrice}")]
    public class KlineRestData
    {
        public decimal BaseAssetVolume { get; set; }

        public decimal ClosePrice { get; set; }

        public DateTime CloseTime { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }

        public long NumberOfTrades { get; set; }

        public decimal OpenPrice { get; set; }

        public DateTime OpenTime { get; set; }

        public decimal QuoteAssetVolume { get; set; }

        public decimal TakerBuyBaseAssetVolume { get; set; }

        public decimal TakerBuyQuoteAssetVolume { get; set; }
    }
}
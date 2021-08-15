namespace HackF5.Binance.Api.Model.Core
{
    using System;

    public interface IKlineItem
    {
        decimal BaseAssetVolume { get; }

        decimal ClosePrice { get; }

        DateTime CloseTime { get; }

        decimal HighPrice { get; }

        decimal LowPrice { get; }

        long NumberOfTrades { get; }

        decimal OpenPrice { get; }

        DateTime OpenTime { get; }

        decimal QuoteAssetVolume { get; }

        decimal TakerBuyBaseAssetVolume { get; }

        decimal TakerBuyQuoteAssetVolume { get; }
    }
}
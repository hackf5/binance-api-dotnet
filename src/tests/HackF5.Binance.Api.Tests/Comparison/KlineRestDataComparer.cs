namespace HackF5.Binance.Api.Tests.Comparison
{
    using HackF5.Binance.Api.Model.Rest.Market;

    public class KlineRestDataComparer : EqualityComparerBase<KlineRestData>
    {
        public static readonly KlineRestDataComparer Instance = new();

        private KlineRestDataComparer()
        {
        }

        protected override bool Equal(KlineRestData x, KlineRestData y)
        {
            return x.BaseAssetVolume == y.BaseAssetVolume
                && x.ClosePrice == y.ClosePrice
                && x.CloseTime == y.CloseTime
                && x.HighPrice == y.HighPrice
                && x.LowPrice == y.LowPrice
                && x.NumberOfTrades == y.NumberOfTrades
                && x.OpenPrice == y.OpenPrice
                && x.OpenTime == y.OpenTime
                && x.QuoteAssetVolume == y.QuoteAssetVolume
                && x.TakerBuyBaseAssetVolume == y.TakerBuyBaseAssetVolume
                && x.TakerBuyQuoteAssetVolume == y.TakerBuyQuoteAssetVolume;
        }
    }
}
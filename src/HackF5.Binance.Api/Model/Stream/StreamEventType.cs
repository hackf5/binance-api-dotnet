namespace HackF5.Binance.Api.Model.Stream
{
    using System.ComponentModel;

    public enum StreamEventType
    {
        [Description("trade")]
        Trade,

        [Description("aggTrade")]
        AggregateTrade,

        [Description("kline")]
        Kline,

        [Description("24hrMiniTicker")]
        MiniTicker24Hr,

        [Description("24hrTicker")]
        Ticker24Hr,

        [Description("bookTicker")]
        BookTicker,

        [Description("miniBookDepth")]
        MiniBookDepth,

        [Description("depthUpdate")]
        BookDepth,
    }
}
namespace HackF5.Binance.Api.Model.Stream
{
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public enum StreamEventType
    {
        [EnumMember(Value = "trade")]
        Trade,

        [EnumMember(Value = "aggTrade")]
        AggregateTrade,

        [EnumMember(Value = "kline")]
        Kline,

        [EnumMember(Value = "24hrMiniTicker")]
        MiniTicker24Hr,

        [EnumMember(Value = "24hrTicker")]
        Ticker24Hr,

        [EnumMember(Value = "bookTicker")]
        BookTicker,

        [EnumMember(Value = "miniBookDepth")]
        MiniBookDepth,

        [EnumMember(Value = "depthUpdate")]
        BookDepth,
    }
}
namespace HackF5.Binance.Api.Request.Stream.Market
{
    using HackF5.Binance.Api.Model.Core;
    using HackF5.Binance.Api.Util;

    public class KlineStreamRequest : SymbolStreamRequest
    {
        public KlineStreamRequest(string symbol, KlineInterval interval)
            : base(symbol) => this.Interval = interval;

        public KlineInterval Interval { get; }

        public override string Parameters => $"kline_{this.Interval.AsEnumMember()}";
    }
}
namespace HackF5.Binance.Api.Request.Stream.Market
{
    using EnumsNET;

    using HackF5.Binance.Api.Model.Core;

    public class KlineStreamRequest : SymbolStreamRequest
    {
        public KlineStreamRequest(string symbol, KlineInterval interval)
            : base(symbol) => this.Interval = interval;

        public KlineInterval Interval { get; }

        public override string Parameters => $"kline_{this.Interval.AsString(EnumFormat.EnumMemberValue)}";
    }
}
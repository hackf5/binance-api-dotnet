namespace HackF5.Binance.Api.Tests.Comparison
{
    using System.Linq;

    using HackF5.Binance.Api.Model.Rest.Market;

    public class OrderBookRestDataEqualityComparer : EqualityComparerBase<OrderBookRestData>
    {
        public static readonly OrderBookRestDataEqualityComparer Instance = new();

        private OrderBookRestDataEqualityComparer()
        {
        }

        protected override bool Equal(OrderBookRestData x, OrderBookRestData y)
        {
            return x.LastUpdateId == y.LastUpdateId
                && x.Asks.SequenceEqual(y.Asks, OrderBookItemEqualityComparer.Instance)
                && x.Bids.SequenceEqual(y.Bids, OrderBookItemEqualityComparer.Instance);
        }
    }
}
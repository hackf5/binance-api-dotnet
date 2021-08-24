namespace HackF5.Binance.Api.Tests.Comparison
{
    using HackF5.Binance.Api.Model.Core;

    public class OrderBookItemEqualityComparer : EqualityComparerBase<OrderBookItem>
    {
        public static readonly OrderBookItemEqualityComparer Instance = new();

        private OrderBookItemEqualityComparer()
        {
        }

        protected override bool Equal(OrderBookItem x, OrderBookItem y) =>
            x.Price == y.Price && x.Quantity == y.Quantity;
    }
}
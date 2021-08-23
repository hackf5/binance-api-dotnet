namespace HackF5.Binance.Api.Response.Rest.Market
{
    using HackF5.Binance.Api.Model.Rest.Market;
    using HackF5.Binance.Api.Request.Rest.Market;

    public class OrderBookRestResponse : ResponseBase<OrderBookRestRequest, OrderBookRestData>
    {
        public OrderBookRestResponse(OrderBookRestRequest request, string json)
            : base(request, json)
        {
        }
    }
}
namespace HackF5.Binance.Api.Response.Rest.Market
{
    using HackF5.Binance.Api.Request.Rest.Market;

    using Up.Crypto.Binance.Model.Rest;

    public class KlineRestResponse : ResponseBase<KlineRestRequest, KlineRestData[]>
    {
        public KlineRestResponse(KlineRestRequest request, string json)
            : base(request, json)
        {
        }
    }
}
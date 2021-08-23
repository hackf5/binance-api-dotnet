﻿namespace HackF5.Binance.Api.Response.Rest.Market
{
    using HackF5.Binance.Api.Model.Rest.Market;
    using HackF5.Binance.Api.Request.Rest.Market;

    public class KlineRestResponse : ResponseBase<KlineRestRequest, KlineRestData[]>
    {
        public KlineRestResponse(KlineRestRequest request, string json)
            : base(request, json)
        {
        }
    }
}
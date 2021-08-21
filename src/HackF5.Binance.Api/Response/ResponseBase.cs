namespace HackF5.Binance.Api.Response
{
    using System;

    using HackF5.Binance.Api.Util;

    public class ResponseBase<TRequest, TPayload>
    {
        public ResponseBase(TRequest request, string json)
            : this(request, JsonSerializer.Deserialize<TPayload>(json))
        {
        }

        private ResponseBase(TRequest request, TPayload payload)
        {
            this.Request = request;
            this.Payload = payload;
        }

        public DateTime Time { get; } = DateTime.UtcNow;

        public TRequest Request { get; }

        public TPayload Payload { get; }
    }
}
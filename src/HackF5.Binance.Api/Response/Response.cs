namespace HackF5.Binance.Api.Response
{
    using System;

    using HackF5.Binance.Api.Util;

    public class Response<TRequest, TPayload>
    {
        public Response(TRequest request, string json)
            : this(request, JsonSerializer.Deserialize<TPayload>(json))
        {
        }

        private Response(TRequest request, TPayload payload)
        {
            this.Request = request;
            this.Payload = payload;
        }

        public DateTime Time { get; } = DateTime.UtcNow;

        public TRequest Request { get; }

        public TPayload Payload { get; }
    }
}
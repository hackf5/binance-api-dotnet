namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Request.Rest;

    public interface IRequestSemaphore : IDisposable
    {
        Task WaitAsync(RestRequest request, CancellationToken cancellation = default);
    }
}
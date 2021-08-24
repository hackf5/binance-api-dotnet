namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using HackF5.Binance.Api.Request.Rest;

    public sealed class RequestSemaphore : IRequestSemaphore
    {
        private const int SpinWaitMilliseconds = 10;

        private const int MaxWeight = 1190;

        private readonly SemaphoreSlim _lock = new(1);

        private readonly Queue<(RestRequest Request, DateTime Time)> _requests = new();

        private int Weight => this._requests.Sum(t => t.Request.Weight);

        public async Task WaitAsync(RestRequest request, CancellationToken cancellation = default)
        {
            await this._lock.WaitAsync(cancellation);

            while (!this.TotalWeightBelowThreshold(request))
            {
                await Task.Delay(
                    TimeSpan.FromMilliseconds(SpinWaitMilliseconds),
                    cancellation);
            }

            this._requests.Enqueue((request, DateTime.UtcNow));
            this._lock.Release();
        }

        public void Dispose() => this._lock.Dispose();

        private bool TotalWeightBelowThreshold(RestRequest request)
        {
            var sentinel = DateTime.UtcNow.AddMinutes(-1);

            while (this._requests.TryPeek(out var t))
            {
                if (t.Time < sentinel)
                {
                    this._requests.Dequeue();
                    continue;
                }

                break;
            }

            return this.Weight + request.Weight < MaxWeight;
        }
    }
}
namespace HackF5.Binance.Api.Response.Stream
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class StreamResponse<TRequest, TPayload> :
        IAsyncEnumerable<ResponseBase<TRequest, TPayload>>,
        IDisposable
        where TRequest : class
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly IAsyncEnumerable<string> _stream;

        private int _sentinel;

        private CancellationTokenSource? _enumeratorCancellationTokenSource;

        protected StreamResponse(
            TRequest request,
            IAsyncEnumerable<string> stream,
            CancellationToken cancellation)
        {
            this.Request = request ?? throw new ArgumentNullException(nameof(request));
            this._stream = stream ?? throw new ArgumentNullException(nameof(stream));
            this._cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
        }

        public bool IsCancelled => this._cancellationTokenSource.IsCancellationRequested;

        public TRequest Request { get; }

        public void Cancel() => this._cancellationTokenSource.Cancel();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IAsyncEnumerator<ResponseBase<TRequest, TPayload>> GetAsyncEnumerator(
            CancellationToken cancellationToken = default)
        {
            if (Interlocked.CompareExchange(ref this._sentinel, 1, 0) != 0)
            {
                throw new InvalidOperationException("This stream has already been enumerated.");
            }

            this._enumeratorCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken,
                this._cancellationTokenSource.Token);

            return new ResponseStreamEnumerator(
                this.Request,
                this._stream.GetAsyncEnumerator(this._enumeratorCancellationTokenSource.Token));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this._cancellationTokenSource.Dispose();
            this._enumeratorCancellationTokenSource?.Dispose();
        }

        private class ResponseStreamEnumerator : IAsyncEnumerator<ResponseBase<TRequest, TPayload>>
        {
            private readonly TRequest _request;

            private readonly IAsyncEnumerator<string> _underlying;

            public ResponseStreamEnumerator(TRequest request, IAsyncEnumerator<string> underlying)
            {
                this._request = request;
                this._underlying = underlying;
            }

            public ResponseBase<TRequest, TPayload> Current
            {
                get
                {
                    var current = this._underlying.Current;
                    return new ResponseBase<TRequest, TPayload>(this._request, current);
                }
            }

            public ValueTask DisposeAsync()
            {
                try
                {
                    return this._underlying.DisposeAsync();
                }
                catch (ObjectDisposedException)
                {
                    return new ValueTask(Task.CompletedTask);
                }
            }

            public ValueTask<bool> MoveNextAsync()
            {
                try
                {
                    return this._underlying.MoveNextAsync();
                }
                catch (OperationCanceledException)
                {
                    return new ValueTask<bool>(Task.FromResult(false));
                }
            }
        }
    }
}
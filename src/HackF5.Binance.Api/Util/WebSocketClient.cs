namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public sealed class WebSocketClient : IDisposable
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
            Formatting = Formatting.None,
        };

        private readonly ClientWebSocket _webSocket = new();

        private readonly SemaphoreSlim _semaphore = new(1);

        private bool _connected;

        public WebSocketClient()
        {
            this._webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(190);
        }

        public async Task ConnectAsync(IEnumerable<string> streamNames, CancellationToken cancellation = default)
        {
            await this._semaphore.WaitAsync(cancellation);
            try
            {
                if (this._connected)
                {
                    throw new InvalidOperationException("Client is already connected.");
                }

                this._connected = true;
                await this._webSocket.ConnectAsync(
                    new Uri("wss://stream.binance.com:9443/stream"), cancellation);

                var request = new StreamRequest
                {
                    Method = "SUBSCRIBE",
                    Params = streamNames.ToArray(),
                    Id = 1,
                };

                var json = JsonConvert.SerializeObject(request, SerializerSettings);
                var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
                await this._webSocket.SendAsync(buffer, WebSocketMessageType.Text, false, cancellation);

                var resultBuffer = WebSocket.CreateClientBuffer(1024, 1024);
                var result = await this._webSocket.ReceiveAsync(resultBuffer, cancellation);

                var builder = new StringBuilder();
                builder.Append(Encoding.UTF8.GetString(resultBuffer.Slice(0, result.Count)));
                var resultJson = builder.ToString();
                var response = JsonConvert.DeserializeObject<StreamResponse>(resultJson);
                if (response is null || response.Id != 1 || response.Result is not null)
                {
                    throw new InvalidOperationException($"Unexpected result JSON: {resultJson}.");
                }
            }
            finally
            {
                this._semaphore.Release();
            }
        }

        public async IAsyncEnumerable<string> GetStreamAsync(
        [EnumeratorCancellation] CancellationToken cancellation = default)
        {
            var buffer = WebSocket.CreateClientBuffer(1024, 1024);
            while (this._webSocket.State == WebSocketState.Open)
            {
                var builder = new StringBuilder();
                while (true)
                {
                    WebSocketReceiveResult result;
                    try
                    {
                        result = await this._webSocket.ReceiveAsync(buffer, cancellation);
                    }
                    catch (OperationCanceledException)
                    {
                        yield break;
                    }

                    builder.Append(Encoding.UTF8.GetString(buffer.Slice(0, result.Count)));

                    if (result.EndOfMessage)
                    {
                        break;
                    }
                }

                yield return builder.ToString();
            }
        }

        public void Dispose()
        {
            this._webSocket.Dispose();
            this._semaphore.Dispose();
        }

        private class StreamRequest
        {
            public string? Method { get; set; }

            public string[] Params { get; set; } = Array.Empty<string>();

            public uint Id { get; set; }
        }

        private class StreamResponse
        {
            public uint Id { get; set; }

            public string? Result { get; set; }
        }
    }
}
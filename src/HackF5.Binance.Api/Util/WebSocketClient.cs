namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;

    public class WebSocketClient : IWebSocketClient
    {
        private static readonly Uri BaseWebsocketUri = new("wss://stream.binance.com:9443/ws");

        public async IAsyncEnumerable<string> GetStreamAsync(
            string streamName,
            [EnumeratorCancellation] CancellationToken cancellation = default)
        {
            var uriBuilder = new UriBuilder(BaseWebsocketUri) { Path = $"/ws/{streamName}" };
            var uri = uriBuilder.Uri;

            using var socket = new ClientWebSocket();
            socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(190);

            await socket.ConnectAsync(uri, cancellation);

            var buffer = WebSocket.CreateClientBuffer(1024, 1024);
            while (socket.State == WebSocketState.Open)
            {
                var builder = new StringBuilder();
                while (true)
                {
                    WebSocketReceiveResult result;
                    try
                    {
                        result = await socket.ReceiveAsync(buffer, cancellation);
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
    }
}
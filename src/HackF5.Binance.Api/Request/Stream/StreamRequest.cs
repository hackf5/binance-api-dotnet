namespace HackF5.Binance.Api.Request.Stream
{
    public abstract class StreamRequest
    {
        public abstract string Path { get; }

        public virtual string Parameters { get; } = string.Empty;
    }
}
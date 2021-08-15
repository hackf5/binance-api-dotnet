namespace HackF5.Binance.Api.Util
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IndexAttribute : Attribute
    {
        public IndexAttribute(int value)
        {
            this.Value = value;
        }

        public int Value { get; }
    }
}
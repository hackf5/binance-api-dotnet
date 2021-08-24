namespace HackF5.Binance.Api.Tests.Comparison
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public abstract class EqualityComparerBase<T> : EqualityComparer<T>
    {
        public sealed override bool Equals(T? x, T? y)
        {
            return ReferenceEquals(x, y) || (x is not null && y is not null && this.Equal(x, y));
        }

        public override int GetHashCode([DisallowNull] T obj) => throw new NotSupportedException();

        protected abstract bool Equal(T x, T y);
    }
}
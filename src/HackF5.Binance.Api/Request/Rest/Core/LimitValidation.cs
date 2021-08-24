namespace HackF5.Binance.Api.Request.Rest.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class LimitValidation
    {
        public static void ValidateRange(int limit, int lower, int upper)
        {
            if (limit < lower)
            {
                throw new ArgumentException(
                    $"Invalid limit: {limit}. Limit must be at least {lower}.",
                    nameof(limit));
            }

            if (limit > upper)
            {
                throw new ArgumentException(
                    $"Invalid limit: {limit}. Limit must be less than or equal to {upper}.",
                    nameof(limit));
            }
        }

        public static void ValidateCollection(int limit, ISet<int> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (!collection.Contains(limit))
            {
                throw new ArgumentException(
                    $"Invalid limit: {limit}. "
                    + $"Valid limits are: {string.Join(", ", collection.OrderBy(x => x))}.",
                    nameof(limit));
            }
        }
    }
}
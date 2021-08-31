namespace HackF5.Binance.Api.Request.Rest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using HackF5.Binance.Api.Request.Rest.Core;
    using HackF5.Binance.Api.Util;

    public abstract class RestRequest
    {
        private static readonly HashSet<string> ExcludedProperties =
            new(new[] { nameof(Path), nameof(Query), nameof(Weight) });

        public abstract string Path { get; }

        public virtual string Query
        {
            get
            {
                var parameters = new List<string>();
                foreach (var property in this.GetType().GetRuntimeProperties().OrderBy(p => p.Name))
                {
                    if (ExcludedProperties.Contains(property.Name))
                    {
                        continue;
                    }

                    var queryParameter = property.GetCustomAttribute<QueryParameterAttribute>();
                    if (queryParameter == null)
                    {
                        continue;
                    }

                    var value = property.GetValue(this);
                    if (value == null)
                    {
                        continue;
                    }

                    if (value.GetType().IsEnum)
                    {
                        value = EnumExtensions.AsEnumMember((dynamic)value);
                    }

                    parameters.Add($"{queryParameter.ParameterName}={value}");
                }

                return parameters.Count == 0 ? string.Empty : string.Join("&", parameters);
            }
        }

        public abstract int Weight { get; }
    }
}
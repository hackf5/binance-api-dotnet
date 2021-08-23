namespace HackF5.Binance.Api.Request.Rest.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class QueryParameterAttribute : Attribute
    {
        public QueryParameterAttribute(string parameterName)
        {
            this.ParameterName = parameterName;
        }

        public string ParameterName { get; }
    }
}
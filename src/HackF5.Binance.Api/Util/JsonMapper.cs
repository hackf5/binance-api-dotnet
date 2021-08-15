namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Globalization;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonMapper
    {
        public static DateTime ToDateTime(long milliseconds) =>
            DateTimeOffset.FromUnixTimeMilliseconds(milliseconds).UtcDateTime;

        public static object Convert(object value, Type type)
        {
            value = (value as JValue)?.Value ?? value;
            if (type == typeof(DateTime))
            {
                switch (value)
                {
                    case int milliseconds:
                        return ToDateTime(milliseconds);
                    case long milliseconds:
                        return ToDateTime(milliseconds);
                    case DateTime:
                        break;
                    default:
                        throw new ArgumentException(
                            $"Value '{value}' is of an unsupported type.",
                            nameof(value));
                }
            }

            return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        public static TModel Map<TModel>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<TModel>(json)
                    ?? throw new ArgumentException(
                        $"JSON '{json}' deserializes to NULL.",
                        nameof(json));
            }
            catch (JsonSerializationException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize '{json}' as '{typeof(TModel)}'.", ex);
            }
        }
    }
}
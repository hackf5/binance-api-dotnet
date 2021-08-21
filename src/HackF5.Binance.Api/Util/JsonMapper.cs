namespace HackF5.Binance.Api.Util
{
    using System;

    using Newtonsoft.Json;

    public static class JsonMapper
    {
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
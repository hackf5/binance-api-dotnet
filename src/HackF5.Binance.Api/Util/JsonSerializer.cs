namespace HackF5.Binance.Api.Util
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class JsonSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
            Formatting = Formatting.None,
        };

        public static TModel Deserialize<TModel>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<TModel>(json, SerializerSettings)
                    ?? throw new ArgumentException(
                        $"JSON '{json}' deserializes to NULL.",
                        nameof(json));
            }
            catch (JsonSerializationException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize '{json}' as '{typeof(TModel)}'.", ex);
            }
        }

        public static string Serialize(object graph)
        {
            return JsonConvert.SerializeObject(graph, SerializerSettings);
        }
    }
}
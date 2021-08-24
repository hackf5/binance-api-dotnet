namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<(Type Type, string Name), string?> EnumToEnumMemberMap = new();

        private static readonly ConcurrentDictionary<(Type Type, string EnumMember), object?> EnumMemberToEnumMap = new();

        public static string AsEnumMember<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            return EnumToEnumMemberMap.GetOrAdd((typeof(TEnum), value.ToString()), o => Factory(o.Name))
                ?? throw new ArgumentException($"Value {value} has no enum member specified.");

            static string? Factory(string name) => typeof(TEnum)
                .GetTypeInfo()
                .DeclaredMembers
                ?.FirstOrDefault(x => x.Name == name)
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }

        public static TEnum FromEnumMember<TEnum>(this string enumMember)
            where TEnum : struct, Enum
        {
            var result = EnumMemberToEnumMap.GetOrAdd((typeof(TEnum), enumMember), o => Factory(o.EnumMember)) as TEnum?;

#pragma warning disable CA1508 // false positive
            return result ?? throw new ArgumentException($"Enum member {enumMember} has no enum member specified.");
#pragma warning restore CA1508

            static TEnum? Factory(string enumMember)
            {
                foreach (var name in Enum.GetNames<TEnum>())
                {
                    var enumMemberAttribute = typeof(TEnum)
                        .GetField(name)
                        !.GetCustomAttributes<EnumMemberAttribute>()
                        .FirstOrDefault();
                    if (enumMemberAttribute?.Value == enumMember)
                    {
                        return Enum.Parse<TEnum>(name);
                    }
                }

                return default;
            }
        }
    }
}
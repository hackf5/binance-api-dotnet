namespace HackF5.Binance.Api.Util
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class IndexMapper
    {
        private static readonly MethodInfo ConvertMethod = typeof(IndexMapper).GetMethod(
            nameof(IndexMapper.Convert),
            BindingFlags.NonPublic | BindingFlags.Static)
                ?? throw new InvalidOperationException($"Cannot find {nameof(IndexMapper.Convert)} method");

        private static readonly ConcurrentDictionary<Type, object> Mappings = new();

        public static void Map<TTarget>(IEnumerable<object> values, TTarget target)
        {
            var mapping = (Action<IReadOnlyList<object>, TTarget>)Mappings.GetOrAdd(
                typeof(TTarget),
                t => CreateMapAction<TTarget>());

            mapping(
                values?.ToArray() ?? throw new ArgumentNullException(nameof(values)),
                target ?? throw new ArgumentNullException(nameof(target)));
        }

        private static object Convert(object value, PropertyInfo property)
        {
            try
            {
                return JsonMapper.Convert(value, property.PropertyType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to convert property {property}", ex);
            }
        }

        private static Action<IReadOnlyList<object>, TTarget> CreateMapAction<TTarget>()
        {
            var targetParameter = Expression.Parameter(typeof(TTarget));
            var valuesParameter = Expression.Parameter(typeof(IReadOnlyList<object>));

            var properties = typeof(TTarget).GetRuntimeProperties()
                .Where(p => p.GetCustomAttribute<IndexAttribute>() != null)
                .ToArray();

            var statements = new List<Expression>();
            foreach (var property in properties)
            {
                var valueIndex = property!.GetCustomAttribute<IndexAttribute>()!.Value;
                var element = Expression.Property(valuesParameter, "Item", Expression.Constant(valueIndex));

                var callConvert = Expression.Call(
                    ConvertMethod,
                    element,
                    Expression.Constant(property));

                var converted = Expression.Convert(callConvert, property.PropertyType);
                statements.Add(Expression.Assign(Expression.Property(targetParameter, property), converted));
            }

            return Expression
                .Lambda<Action<IReadOnlyList<object>, TTarget>>(
                    Expression.Block(statements),
                    valuesParameter,
                    targetParameter)
                .Compile();
        }
    }
}
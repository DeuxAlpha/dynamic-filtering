using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Filtering.Models;

namespace Filtering
{
    internal static class ExpressionRetriever
    {
        private static readonly MethodInfo ContainsMethod =
            typeof(string).GetMethod(nameof(string.Contains), new[] {typeof(string)});

        private static readonly MethodInfo StartsWithMethod =
            typeof(string).GetMethod(nameof(string.StartsWith), new[] {typeof(string)});

        private static readonly MethodInfo EndsWithMethod =
            typeof(string).GetMethod(nameof(string.EndsWith), new[] {typeof(string)});

        internal static Expression GetExpression(ParameterExpression parameter, ExpressionFilter filter)
        {
            return GetComparingExpression(
                Expression.Property(parameter, filter.PropertyName),
                filter.Value == null ?
                    (Expression) Expression.Constant(null) :
                    GetValueExpression(filter, Expression.Property(parameter, filter.PropertyName)),
                filter);
        }

        private static Expression GetComparingExpression(Expression left, Expression right, ExpressionFilter filter)
        {
            switch (filter.Comparison)
            {
                case Comparison.Equal:
                    return Expression.Equal(left, right);
                case Comparison.LessThan:
                    return Expression.LessThan(left, right);
                case Comparison.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);
                case Comparison.GreaterThan:
                    return Expression.GreaterThan(left, right);
                case Comparison.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case Comparison.NotEqual:
                    return Expression.NotEqual(left, right);
                case Comparison.Contains:
                    return Expression.Call(left, ContainsMethod, right);
                case Comparison.StartsWith:
                    return Expression.Call(left, StartsWithMethod, right);
                case Comparison.EndsWith:
                    return Expression.Call(left, EndsWithMethod, right);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Dynamically casts the provided filter to the type that it will be compared to.
        /// </summary>
        private static UnaryExpression GetValueExpression(ExpressionFilter filter, MemberExpression member)
        {
            var propertyType = ((PropertyInfo) member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType);
            if (!converter.CanConvertFrom(typeof(string)))
                throw new NotSupportedException();
            var propertyValue = converter.ConvertFromInvariantString(filter.Value.ToString());
            var constant = Expression.Constant(propertyValue);
            var valueExpression = Expression.Convert(constant, propertyType);
            return valueExpression;
        }
    }
}
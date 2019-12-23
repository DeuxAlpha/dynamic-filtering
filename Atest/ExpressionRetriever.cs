using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Atest
{
    public class ExpressionRetriever
    {
        private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod(nameof(string.Contains));

        private static readonly MethodInfo StartsWithMethod =
            typeof(string).GetMethod(nameof(string.StartsWith), new[] {typeof(string)});

        private static readonly MethodInfo EndsWithMethod =
            typeof(string).GetMethod(nameof(string.EndsWith), new[] {typeof(string)});

        public static Expression GetExpression<T>(ParameterExpression parameter, ExpressionFilter filter)
        {
            var member = Expression.Property(parameter, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);
            switch (filter.Comparison)
            {
                case Comparison.Equal:
                    return Expression.Equal(member, constant);
                case Comparison.LessThan:
                    return Expression.LessThan(member, constant);
                case Comparison.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);
                case Comparison.GreaterThan:
                    return Expression.GreaterThan(member, constant);
                case Comparison.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);
                case Comparison.NotEqual:
                    return Expression.NotEqual(member, constant);
                case Comparison.Contains:
                    return Expression.Call(member, ContainsMethod, constant);
                case Comparison.StartsWith:
                    return Expression.Call(member, StartsWithMethod, constant);
                case Comparison.EndsWith:
                    return Expression.Call(member, EndsWithMethod, constant);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
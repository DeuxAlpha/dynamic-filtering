using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Filtering.Models;

namespace Filtering
{
    public static class ExpressionCreator
    {
        public static IEnumerable<Expression<Func<T, bool>>> Build<T>(List<ExpressionFilter> filters)
        {
            return YieldFilters(filters)
                .Select(ConstructExpressionTree<T>);
        }

        private static Expression<Func<T, bool>> ConstructExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0) return null;

            var parameter = Expression.Parameter(typeof(T));
            var expression = ExpressionRetriever.GetExpression(parameter, filters[0]);

            for (var i = 1; i < filters.Count; i++)
            {
                expression = Expression.And(
                    expression,
                    ExpressionRetriever.GetExpression(parameter, filters[i]));
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        /// <summary>
        /// Returns a list of filters divided by their relations. Filters that are related via an 'or'
        /// need to be handled distinctively, because they query the source independently of the previous filters.
        /// </summary>
        private static IEnumerable<List<ExpressionFilter>> YieldFilters(
            IReadOnlyList<ExpressionFilter> expressionFilters)
        {
            var filtersToReturn = new List<ExpressionFilter>();
            for (var i = 0; i < expressionFilters.Count; i++)
            {
                var filter = expressionFilters[i];
                filtersToReturn.Add(filter);
                if (i + 1 >= expressionFilters.Count || expressionFilters[i + 1].Relation != Relation.Or) continue;
                yield return filtersToReturn;
                filtersToReturn = new List<ExpressionFilter>();
            }

            yield return filtersToReturn;
        }
    }
}
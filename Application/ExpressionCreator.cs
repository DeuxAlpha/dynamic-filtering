using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application
{
    public class ExpressionCreator
    {
        public static List<Expression<Func<T, bool>>> ConstructExpressionTrees<T>(List<ExpressionFilter> filters)
        {
            return YieldFilters(filters)
                .Select(ConstructExpressionTree<T>)
                .ToList();
        }

        private static Expression<Func<T, bool>> ConstructExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0) return null;

            var parameter = Expression.Parameter(typeof(T));
            var expression = ExpressionRetriever.GetExpression<T>(parameter, filters[0]);

            for (var i = 1; i < filters.Count; i++)
            {
                expression = GetExpressionRelation(filters[i].Relation)(
                    expression,
                    ExpressionRetriever.GetExpression<T>(parameter, filters[i]));
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        private static Func<Expression, Expression, BinaryExpression> GetExpressionRelation(Relation relation)
        {
            switch (relation)
            {
                case Relation.And: return Expression.And;
                case Relation.Or: return Expression.Or;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Returns a list of filters divided by their relations. Filters that are related via an 'or', plus a following
        /// 'and' need to be handled distinctively, because they query the source independently of the previous filters.
        /// </summary>
        private static IEnumerable<List<ExpressionFilter>> YieldFilters(
            IReadOnlyList<ExpressionFilter> expressionFilters)
        {
            var filtersToReturn = new List<ExpressionFilter>();
            for (var i = 0; i < expressionFilters.Count; i++)
            {
                var filter = expressionFilters[i];
                filtersToReturn.Add(filter);
                if (expressionFilters.Count <= i + 1 || expressionFilters[i + 1].Relation != Relation.Or) continue;
                yield return filtersToReturn;
                filtersToReturn = new List<ExpressionFilter>();
            }

            yield return filtersToReturn;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Atest
{
    public class ExpressionCreator
    {
        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0) return null;

            var parameter = Expression.Parameter(typeof(T), "t");
            Expression expression = null;

            if (filters.Count == 1)
                expression = ExpressionRetriever.GetExpression<T>(parameter, filters[0]);

            else
            {
                expression = ExpressionRetriever.GetExpression<T>(parameter, filters[0]);
                for (var i = 1; i < filters.Count; i++)
                {
                    expression = Expression.And(expression,
                        ExpressionRetriever.GetExpression<T>(parameter, filters[i]));
                }
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
    }
}
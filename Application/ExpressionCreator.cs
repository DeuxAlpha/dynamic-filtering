using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application
{
    public class ExpressionCreator
    {
        public static Expression<Func<T, bool>> ConstructAndExpressionTree<T>(List<ExpressionFilter> filters)
        {
            if (filters.Count == 0) return null;

            var parameter = Expression.Parameter(typeof(T));
            Expression expression;

            if (filters.Count == 1)
                expression = ExpressionRetriever.GetExpression<T>(parameter, filters[0]);

            else
            {
                expression = ExpressionRetriever.GetExpression<T>(parameter, filters[0]);
                for (var i = 1; i < filters.Count; i++)
                {
                    expression = Expression.Or(expression,
                        ExpressionRetriever.GetExpression<T>(parameter, filters[i]));
                }
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
    }
}
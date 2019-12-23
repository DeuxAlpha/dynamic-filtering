using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public static class Filter
    {
        public static IEnumerable<T> MultipleDynamic<T>(
            Func<Func<T,bool>,IEnumerable<T>> clause,
            List<ExpressionFilter> filters)
        {
            var overallCollection = new List<T>();
            var expressions = ExpressionCreator.ConstructExpressionTrees<T>(filters);
            foreach (var expression in expressions)
            {
                overallCollection.AddRange(clause(expression.Compile()));
            }

            return overallCollection.Distinct();
        }
    }
}
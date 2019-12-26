using Filtering.Models;

namespace Filtering
{
    public class ExpressionFilter
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
        public Comparison Comparison { get; set; } = Comparison.Equal;
        public Relation Relation { get; set; } = Relation.And;

        public ExpressionFilter(){}

        public ExpressionFilter(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}
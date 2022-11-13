using QueryingOmega.Querying.Filtering.Enums;

namespace QueryingOmega.Extensions
{
    internal static class ComparisonExtensions
    {
        public static bool IsStringComparison(this ComparisonEnum comparisonEnum)
        {
            return comparisonEnum is ComparisonEnum.Contains or ComparisonEnum.StartsWith or ComparisonEnum.EndsWith;
        }
    }
}
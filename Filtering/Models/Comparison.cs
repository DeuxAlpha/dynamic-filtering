namespace Filtering.Models
{
    public enum Comparison
    {
        Equal,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        NotEqual,
        Contains, //for strings
        StartsWith, //for strings
        EndsWith //for strings
    }
}
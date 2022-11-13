using System.Collections.Generic;
using QueryingOmega.Querying.Aggregate;
using QueryingOmega.Querying.Distinct;

namespace QueryingOmega.Querying.Models
{
    public class QueryResponse<T>
    {
        public IEnumerable<Distinction> Distinctions { get; set; } = new List<Distinction>();
        public IEnumerable<Aggregation> Aggregations { get; set; } = new List<Aggregation>();
        public int ItemCount { get; set; }
        public int MaxItemCount { get; set; }
        public int Page { get; set; }
        public int MaxPage { get; set; }
        public int StartItemCount { get; set; }
        public int EndItemCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
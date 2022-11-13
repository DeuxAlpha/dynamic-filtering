using System.Collections.Generic;
using QueryingOmega.Querying.Aggregate;
using QueryingOmega.Querying.Distinct;
using QueryingOmega.Querying.Filtering;
using QueryingOmega.Querying.Sorting;

namespace QueryingOmega.Querying.Models
{
    // TODO: Provide Request DTO to convert string to Enums (e.g. Contains to 7)
    public class QueryRequest
    {
        public IEnumerable<Filter> Filters { get; set; } = new List<Filter>();
        public IEnumerable<Sorter> Sorters { get; set; } = new List<Sorter>();
        public IEnumerable<Aggregator> Aggregators { get; set; } = new List<Aggregator>();
        public IEnumerable<Distinction> Distinctions { get; set; } = new List<Distinction>();
        public int Page { get; set; } = 1;
        public int Items { get; set; } = 30;
    }
}
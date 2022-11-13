using System.Collections.Generic;

namespace QueryingOmega.Querying.Distinct
{
    public class Distinction
    {
        public string PropertyName { get; set; }
        public IEnumerable<object> Values { get; set; }
    }
}
using System;
using QueryingOmega.Querying.Sorting.Enums;

namespace QueryingOmega.Querying.Sorting
{
    public class Sorter
    {
        public string PropertyName { get; set; }
        public string SortDirection { get; set; } = SortDirectionEnum.Ascending.ToString("F");
        internal SortDirectionEnum SortDirectionEnum => Enum.Parse<SortDirectionEnum>(SortDirection, true);
    }
}
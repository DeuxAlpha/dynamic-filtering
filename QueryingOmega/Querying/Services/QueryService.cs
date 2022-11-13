using System.Linq;
using System.Threading.Tasks;
using QueryingOmega.Expressions;
using QueryingOmega.Querying.Models;
using QueryingOmega.Extensions;

namespace QueryingOmega.Querying.Services
{
    public class QueryService
    {
        public static QueryResponse<T> GetQueryResponse<T>(IQueryable<T> source, QueryRequest queryRequest)
        {
            var filtered = source.FilterBy(queryRequest.Filters);
            var ordered = filtered.OrderBy(queryRequest.Sorters);
            var response = QueryExpressions.PaginateRequest(ordered, queryRequest);
            FinishResponse(filtered, queryRequest, response);
            return response;
        }

        public static Task<QueryResponse<T>> GetQueryResponseAsync<T>(IQueryable<T> source, QueryRequest queryRequest)
        {
            return Task.Run(() => GetQueryResponse(source, queryRequest));
        }

        private static void FinishResponse<T>(
            IQueryable<T> source,
            QueryRequest queryRequest,
            QueryResponse<T> queryResponse)
        {
            var filtered = source.FilterBy(queryRequest.Filters);
            var _ = filtered
                .AsEnumerable()
                .GroupBy(item => 1)
                .Select(grouping => QueryExpressions.FinishRequest(grouping, queryRequest, queryResponse))
                .ToList();
        }
    }
}
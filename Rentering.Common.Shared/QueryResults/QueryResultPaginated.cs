namespace Rentering.Common.Shared.QueryResults
{
    public class QueryResultPaginated<TData> : QueryResult<TData> where TData : IDataResult
    {
        public PaginationResult Pagination { get; set; }
    }
}

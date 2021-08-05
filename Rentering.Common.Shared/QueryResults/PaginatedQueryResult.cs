using System.Collections.Generic;

namespace Rentering.Common.Shared.QueryResults
{
    public class PaginatedQueryResult<TData> : ListQueryResult<TData> where TData : IDataResult
    {
        public PaginatedQueryResult(IEnumerable<TData> data, PaginationResult pagination) : base(data)
        {
            Pagination = pagination;
        }

        public PaginationResult Pagination { get; set; }
    }
}

using Rentering.Common.Shared.Data.Interfaces;
using System.Collections.Generic;

namespace Rentering.Common.Shared.Data.QueryResults
{
    public class PaginatedQueryResult<TData> : ListQueryResult<TData> where TData : IDataResult
    {
        public PaginatedQueryResult(IEnumerable<TData> data, PaginationResult pagination) : base(data)
        {
            Pagination = pagination;
        }

        public PaginationResult Pagination { get; set; }
    }

    public class PaginationResult
    {
        public PaginationResult(int page, int recordsPerPage, int totalRecords)
        {
            Page = page;
            RecordsPerPage = recordsPerPage;
            TotalRecords = totalRecords;
        }

        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalRecords { get; set; }
    }
}

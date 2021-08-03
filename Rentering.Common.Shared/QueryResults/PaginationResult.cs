namespace Rentering.Common.Shared.QueryResults
{
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

namespace Rentering.Common.Shared.QueryResults
{
    public class QueryResult<TData> where TData : IDataResult
    {
        public TData Data { get; set; }
    }
}

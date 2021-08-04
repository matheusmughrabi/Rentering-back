namespace Rentering.Common.Shared.QueryResults
{
    public class SingleQueryResult<TData> : IQueryResult where TData : IDataResult
    {
        public SingleQueryResult(TData data)
        {
            Data = data;
        }

        public TData Data { get; set; }
    }
}

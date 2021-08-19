using Rentering.Common.Shared.Data.Interfaces;

namespace Rentering.Common.Shared.Data.QueryResults
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

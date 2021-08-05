using System.Collections.Generic;

namespace Rentering.Common.Shared.QueryResults
{
    public class ListQueryResult<TData> : IQueryResult where TData : IDataResult
    {
        public ListQueryResult(IEnumerable<TData> data)
        {
            Data = data;
        }

        public IEnumerable<TData> Data { get; set; }
    }
}

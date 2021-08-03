using Rentering.Common.Shared.QueryResults;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetCorporationsQueryResult : IDataResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
    }
}

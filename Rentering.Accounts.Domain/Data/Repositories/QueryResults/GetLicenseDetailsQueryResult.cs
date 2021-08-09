using Rentering.Common.Shared.QueryResults;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryResults
{
    public class GetLicenseDetailsQueryResult : IDataResult
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

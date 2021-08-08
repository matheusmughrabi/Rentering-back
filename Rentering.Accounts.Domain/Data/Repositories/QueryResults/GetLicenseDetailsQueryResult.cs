using Rentering.Accounts.Domain.Enums;
using Rentering.Common.Shared.Enums;
using Rentering.Common.Shared.QueryResults;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryResults
{
    public class GetLicenseDetailsQueryResult : IDataResult
    {
        public EnumResult<e_License> License { get; set; }
        public decimal Price { get; set; }
    }
}

using Rentering.Accounts.Domain.Data.Repositories.QueryResults;
using Rentering.Common.Shared.QueryResults;

namespace Rentering.Accounts.Domain.Data.Repositories
{
    public interface IAccountQueryRepository
    {
        GetAccountQueryResult GetAccountById(int id);
        SingleQueryResult<GetLicenseDetailsQueryResult> GetLicenseDetails(int licenseId);
    }
}

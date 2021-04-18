using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface ITenantQueryRepository
    {
        GetTenantQueryResult GetTenantById(int id);
        IEnumerable<GetTenantQueryResult> GetTenantProfilesOfCurrentUser(int accountId);
    }
}

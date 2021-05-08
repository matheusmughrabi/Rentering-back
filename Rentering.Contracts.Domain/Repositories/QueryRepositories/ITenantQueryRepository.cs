using Rentering.Common.Shared.Repositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface ITenantQueryRepository : IGenericQueryRepository<GetTenantQueryResult>
    {
        bool CheckIfAccountExists(int accountId);
        IEnumerable<GetTenantQueryResult> GetTenantProfilesOfCurrentUser(int accountId);
    }
}

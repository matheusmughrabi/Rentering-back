using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories
{
    public interface ITenantQueryRepository : IGenericQueryRepository<GetTenantQueryResult>
    {
        bool CheckIfAccountExists(int accountId);
        IEnumerable<GetTenantQueryResult> GetTenantProfilesOfCurrentUser(int accountId);
    }
}

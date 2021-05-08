using Rentering.Common.Shared.Repositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IRenterQueryRepository : IGenericQueryRepository<GetRenterQueryResult>
    {
        bool CheckIfAccountExists(int accountId);
        IEnumerable<GetRenterQueryResult> GetRenterProfilesOfCurrentUser(int accountId);
    }
}

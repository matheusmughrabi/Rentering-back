using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IRenterQueryRepository
    {
        GetRenterQueryResult GetRenterById(int id);
        IEnumerable<GetRenterQueryResult> GetRenterProfilesOfCurrentUser(int accountId);
    }
}

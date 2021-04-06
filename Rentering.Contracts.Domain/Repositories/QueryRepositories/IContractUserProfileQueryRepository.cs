using Rentering.Contracts.Application.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractUserProfileQueryRepository
    {
        GetContractUserProfileQueryResult GetUserById(int id);
        IEnumerable<GetContractUserProfileQueryResult> GetCurrentUserProfiles(int accountId);
    }
}

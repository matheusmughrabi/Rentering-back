using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IGuarantorQueryRepository
    {
        bool CheckIfAccountExists(int accountId);
        GetGuarantorQueryResult GetGuarantorById(int id);
        IEnumerable<GetGuarantorQueryResult> GetGuarantorProfilesOfCurrentUser(int accountId);
    }
}

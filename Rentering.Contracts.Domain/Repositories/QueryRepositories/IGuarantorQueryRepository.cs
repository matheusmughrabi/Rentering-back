using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IGuarantorQueryRepository
    {
        GetGuarantorQueryResult GetGuarantorById(int id);
        IEnumerable<GetGuarantorQueryResult> GetGuarantorProfilesOfCurrentUser(int accountId);
    }
}

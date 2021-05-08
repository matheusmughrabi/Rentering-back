using Rentering.Common.Shared.Repositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IGuarantorQueryRepository : IGenericQueryRepository<GetGuarantorQueryResult>
    {
        bool CheckIfAccountExists(int accountId);
        IEnumerable<GetGuarantorQueryResult> GetGuarantorProfilesOfCurrentUser(int accountId);
    }
}

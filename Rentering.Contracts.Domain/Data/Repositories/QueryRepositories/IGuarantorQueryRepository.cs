using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories
{
    public interface IGuarantorQueryRepository : IGenericQueryRepository<GetGuarantorQueryResult>
    {
        bool CheckIfAccountExists(int accountId);
        IEnumerable<GetGuarantorQueryResult> GetGuarantorProfilesOfCurrentUser(int accountId);
    }
}

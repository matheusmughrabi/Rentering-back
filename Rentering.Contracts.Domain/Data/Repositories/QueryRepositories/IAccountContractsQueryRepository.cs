using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories
{
    public interface IAccountContractsQueryRepository : IGenericQueryRepository<GetAccountContractsQueryResults>
    {
        bool CheckIfAccountExists(int accountId);
    }
}

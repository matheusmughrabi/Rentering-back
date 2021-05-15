using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories
{
    public interface IEstateContractQueryRepository : IGenericQueryRepository<GetEstateContractQueryResult>
    {
        bool CheckIfContractNameExists(string contractName);
        GetCurrentUserContract GetContract(int contractId);
    }
}

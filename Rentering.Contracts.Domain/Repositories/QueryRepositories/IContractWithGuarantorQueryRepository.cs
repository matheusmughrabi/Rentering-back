using Rentering.Common.Shared.Repositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractWithGuarantorQueryRepository : IGenericQueryRepository<GetContractWithGuarantorQueryResult>
    {
        bool CheckIfContractNameExists(string contractName);
    }
}

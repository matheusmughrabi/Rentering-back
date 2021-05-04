using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractWithGuarantorQueryRepository
    {
        bool CheckIfContractNameExists(string contractName);
        IEnumerable<GetContractWithGuarantorQueryResult> GetAllContracts();
        GetContractWithGuarantorQueryResult GetContractById(int id);
    }
}

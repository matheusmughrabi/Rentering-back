using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.CUDRepositories
{
    public interface IContractCUDRepository
    {
        ContractEntity GetContractForCUD(int contractId);
        bool ContractNameExists(string contractName);
        ContractEntity Add(ContractEntity contractEntity);
        ContractEntity Delete(ContractEntity contractEntity);
        ContractEntity Delete(int id);
    }
}

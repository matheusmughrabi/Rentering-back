using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.CUDRepositories
{
    public interface IEstateContractCUDRepository
    {
        ContractEntity GetEstateContractForCUD(int estateContractId);
        bool ContractNameExists(string contractName);
        ContractEntity Add(ContractEntity estateContractEntity);
        ContractEntity Delete(ContractEntity estateContractEntity);
        ContractEntity Delete(int id);
    }
}

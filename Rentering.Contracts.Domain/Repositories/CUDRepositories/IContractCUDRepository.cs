using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractCUDRepository
    {
        bool CheckIfContractUserProfileExists(int id);
        bool CheckIfContractNameExists(string contractName);
        ContractEntity GetContractById(int id);
        void CreateContract(ContractEntity contract);
        void UpdateContractRentPrice(int id, ContractEntity contract);
        void DeleteContract(int id);
    }
}

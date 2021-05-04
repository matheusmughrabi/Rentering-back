using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractWithGuarantorCUDRepository
    {
        void CreateContract(ContractWithGuarantorEntity contract);
        void UpdateContract(int id, ContractWithGuarantorEntity contract);
        void DeleteContract(int id);
    }
}

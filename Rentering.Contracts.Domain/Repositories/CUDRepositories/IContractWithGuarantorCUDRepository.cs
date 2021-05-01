using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractWithGuarantorCUDRepository
    {
        void CreateContract(ContractWithGuarantorEntity contract);
    }
}

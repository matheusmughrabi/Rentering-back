using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractWithGuarantorCUDRepository
    {
        ContractWithGuarantorEntity GetContractById(int id);
        void CreateContract(ContractWithGuarantorEntity contract);
        void InviteRenterToParticipate(ContractWithGuarantorEntity contract);
    }
}

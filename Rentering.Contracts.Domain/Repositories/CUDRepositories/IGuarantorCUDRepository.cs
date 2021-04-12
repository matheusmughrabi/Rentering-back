using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IGuarantorCUDRepository
    {
        bool CheckIfAccountExists(int accountId);
        void CreateGuarantor(GuarantorEntity guarantor);
    }
}

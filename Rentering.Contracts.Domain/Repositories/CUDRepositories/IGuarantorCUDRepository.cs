using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IGuarantorCUDRepository
    {
        void CreateGuarantor(GuarantorEntity guarantor);
        void UpdateGuarantor(int id, GuarantorEntity guarantor);
        void DeleteGuarantor(int id);
    }
}

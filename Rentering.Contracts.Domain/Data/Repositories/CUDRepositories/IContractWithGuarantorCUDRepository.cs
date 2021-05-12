using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories
{
    public interface IContractWithGuarantorCUDRepository : IGenericCUDRepository<ContractWithGuarantorEntity>
    {
        ContractWithGuarantorEntity GetContractForCUD(int id);
    }
}

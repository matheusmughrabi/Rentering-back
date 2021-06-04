using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.CUDRepositories
{
    public interface IAccountContractCUDRepository
    {
        AccountContractsEntity GetAccountContractForCUD(int accountId, int contractId);
    }
}

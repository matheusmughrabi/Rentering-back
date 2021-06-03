using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.DataEF.CUDRepositories
{
    public interface IAccountContractCUDRepositoryEF
    {
        AccountContractsEntity GetAccountContractForCUD(int accountId, int contractId);
    }
}

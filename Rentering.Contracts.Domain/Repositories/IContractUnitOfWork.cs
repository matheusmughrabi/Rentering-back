using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.Contracts.Domain.Repositories
{
    public interface IContractUnitOfWork
    {
        IRenterCUDRepository Renter { get; }
        ITenantCUDRepository Tenant { get; }
        IGuarantorCUDRepository Guarantor { get; }
        IContractWithGuarantorCUDRepository ContractWithGuarantor { get; }
        IContractPaymentCUDRepository ContractPayment { get; }
    }
}

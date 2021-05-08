using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.Contracts.Domain.Repositories
{
    public interface IContractUnitOfWork
    {
        IRenterCUDRepository RenterCUD { get; }
        IRenterQueryRepository RenterQuery { get; }

        ITenantCUDRepository TenantCUD { get; }
        ITenantQueryRepository TenantQuery { get; }

        IGuarantorCUDRepository GuarantorCUD { get; }
        IGuarantorQueryRepository GuarantorQuery { get; }

        IContractWithGuarantorCUDRepository ContractWithGuarantorCUD { get; }
        IContractWithGuarantorQueryRepository ContractWithGuarantorQuery { get; }

        IContractPaymentCUDRepository ContractPaymentCUD { get; }
        IContractPaymentQueryRepository ContractPaymentQuery { get; }
    }
}

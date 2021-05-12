using Rentering.Common.Shared.Data;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;

namespace Rentering.Contracts.Domain.Data
{
    public interface IContractUnitOfWork : IUnitOfWork
    {
        IRenterCUDRepository RenterCUD { get; }
        IRenterQueryRepository RenterQuery { get; }

        ITenantCUDRepository TenantCUD { get; }
        ITenantQueryRepository TenantQuery { get; }

        IGuarantorCUDRepository GuarantorCUD { get; }
        IGuarantorQueryRepository GuarantorQuery { get; }

        IEstateContractCUDRepository EstateContractCUD { get; }
        IEstateContractQueryRepository EstateContractQuery { get; }

        IContractPaymentCUDRepository ContractPaymentCUD { get; }
        IContractPaymentQueryRepository ContractPaymentQuery { get; }
    }
}

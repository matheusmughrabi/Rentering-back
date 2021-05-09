using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;

namespace Rentering.Contracts.Infra.Repositories
{
    public class ContractUnitOfWork : BaseUnitOfWork, IContractUnitOfWork
    {
        public ContractUnitOfWork(
            RenteringDataContext renteringDataContext,
            IRenterCUDRepository renterCUD,
            IRenterQueryRepository renterQuery,
            ITenantCUDRepository tenantCUD,
            ITenantQueryRepository tenantQuery,
            IGuarantorCUDRepository guarantorCUD,
            IGuarantorQueryRepository guarantorQuery,
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepositoryCUD,
            IContractWithGuarantorQueryRepository contractWithGuarantorQueryRepositoryQuery, 
            IContractPaymentCUDRepository contractPaymentCUD,
            IContractPaymentQueryRepository contractPaymentQuery) : base(renteringDataContext)
        {
            RenterCUD = renterCUD;
            RenterQuery = renterQuery;

            TenantCUD = tenantCUD;
            TenantQuery = tenantQuery;

            GuarantorCUD = guarantorCUD;
            GuarantorQuery = guarantorQuery;

            ContractWithGuarantorCUD = contractWithGuarantorCUDRepositoryCUD;
            ContractWithGuarantorQuery = contractWithGuarantorQueryRepositoryQuery;

            ContractPaymentCUD = contractPaymentCUD;
            ContractPaymentQuery = contractPaymentQuery;
        }

        public IRenterCUDRepository RenterCUD { get; }
        public IRenterQueryRepository RenterQuery { get; }

        public ITenantCUDRepository TenantCUD { get; }
        public ITenantQueryRepository TenantQuery { get; }

        public IGuarantorCUDRepository GuarantorCUD { get; }
        public IGuarantorQueryRepository GuarantorQuery { get; }

        public IContractWithGuarantorCUDRepository ContractWithGuarantorCUD { get; }
        public IContractWithGuarantorQueryRepository ContractWithGuarantorQuery { get; }

        public IContractPaymentCUDRepository ContractPaymentCUD { get; }
        public IContractPaymentQueryRepository ContractPaymentQuery { get; }
    }
}

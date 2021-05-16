using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;

namespace Rentering.Contracts.Infra.Data
{
    public class ContractUnitOfWork : BaseUnitOfWork, IContractUnitOfWork
    {
        public ContractUnitOfWork(
            RenteringDataContext renteringDataContext,
            IAccountContractsCUDRepository accountContractsCUD,
            IRenterCUDRepository renterCUD,
            IRenterQueryRepository renterQuery,
            ITenantCUDRepository tenantCUD,
            ITenantQueryRepository tenantQuery,
            IGuarantorCUDRepository guarantorCUD,
            IGuarantorQueryRepository guarantorQuery,
            IEstateContractCUDRepository estateContractCUDRepositoryCUD,
            IEstateContractQueryRepository estateContractQueryRepositoryQuery,
            IContractPaymentCUDRepository contractPaymentCUD,
            IContractPaymentQueryRepository contractPaymentQuery) : base(renteringDataContext)
        {
            AccountContractsCUD = accountContractsCUD;

            RenterCUD = renterCUD;
            RenterQuery = renterQuery;

            TenantCUD = tenantCUD;
            TenantQuery = tenantQuery;

            GuarantorCUD = guarantorCUD;
            GuarantorQuery = guarantorQuery;

            EstateContractCUD = estateContractCUDRepositoryCUD;
            EstateContractQuery = estateContractQueryRepositoryQuery;

            ContractPaymentCUD = contractPaymentCUD;
            ContractPaymentQuery = contractPaymentQuery;
        }

        public IAccountContractsCUDRepository AccountContractsCUD { get; }

        public IRenterCUDRepository RenterCUD { get; }
        public IRenterQueryRepository RenterQuery { get; }

        public ITenantCUDRepository TenantCUD { get; }
        public ITenantQueryRepository TenantQuery { get; }

        public IGuarantorCUDRepository GuarantorCUD { get; }
        public IGuarantorQueryRepository GuarantorQuery { get; }

        public IEstateContractCUDRepository EstateContractCUD { get; }
        public IEstateContractQueryRepository EstateContractQuery { get; }

        public IContractPaymentCUDRepository ContractPaymentCUD { get; }
        public IContractPaymentQueryRepository ContractPaymentQuery { get; }
    }
}

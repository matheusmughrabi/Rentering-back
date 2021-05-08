using Rentering.Contracts.Domain.Repositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;

namespace Rentering.Contracts.Infra.Repositories
{
    public class ContractUnitOfWork : IContractUnitOfWork
    {
        public ContractUnitOfWork(
            IRenterCUDRepository renter,
            ITenantCUDRepository tenant,
            IGuarantorCUDRepository guarantor,
            IContractWithGuarantorCUDRepository contractWithGuarantorCUDRepository, 
            IContractPaymentCUDRepository contractPayment)
        {
            Renter = renter;
            Tenant = tenant;
            Guarantor = guarantor;
            ContractWithGuarantor = contractWithGuarantorCUDRepository;
            ContractPayment = contractPayment;
        }

        public IRenterCUDRepository Renter { get; }
        public ITenantCUDRepository Tenant { get; }
        public IGuarantorCUDRepository Guarantor { get; }
        public IContractWithGuarantorCUDRepository ContractWithGuarantor { get; }
        public IContractPaymentCUDRepository ContractPayment { get; }
    }
}

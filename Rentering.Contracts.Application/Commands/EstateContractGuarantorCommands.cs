using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateEstateContractGuarantorCommand : ICommand
    {
        public string ContractName { get; private set; }
        public decimal RentPrice { get; private set; }
        public int RenterId { get; private set; }
        public int TenantId { get; private set; }
        public int GuarantorId { get; private set; }
    }
}

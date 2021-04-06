using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Authorization.Commands
{
    public class AuthContractRenterCommand : ICommand
    {
        public AuthContractRenterCommand(int contractId, int currentAccountId)
        {
            ContractId = contractId;
            CurrentAccountId = currentAccountId;
        }

        public int ContractId { get; set; }
        public int CurrentAccountId { get; set; }
    }

    public class AuthContractTenantCommand : ICommand
    {
        public AuthContractTenantCommand(int contractId, int currentAccountId)
        {
            ContractId = contractId;
            CurrentAccountId = currentAccountId;
        }

        public int ContractId { get; set; }
        public int CurrentAccountId { get; set; }
    }

    public class AuthRenterCommand : ICommand
    {
        public AuthRenterCommand(int renterId)
        {
            RenterId = renterId;
        }

        public int RenterId { get; set; }
    }
}

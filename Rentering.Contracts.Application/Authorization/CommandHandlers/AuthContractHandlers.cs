using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Domain.Services;

namespace Rentering.Contracts.Application.Authorization.CommandHandlers
{
    public class AuthContractHandlers : Notifiable, 
        ICommandHandler<AuthContractRenterCommand>
    {
        private readonly IAuthContractService _contractService;

        public AuthContractHandlers(IAuthContractService contractService)
        {
            _contractService = contractService;
        }

        public ICommandResult Handle(AuthContractRenterCommand command)
        {
            bool isCurrentUserContractRenter = _contractService.IsCurrentUserContractRenter(command.CurrentAccountId, command.ContractId);

            if (isCurrentUserContractRenter == false)
                AddNotification("AuthenticatedUserId", "Current user is not the contract renter");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "User is authorized", null);
        }

        public ICommandResult Handle(AuthContractTenantCommand command)
        {
            // Validates if the logged user is the renter of the contract
            bool isCurrentUserContractRenter = _contractService.IsCurrentUserContractTenant(command.CurrentAccountId, command.ContractId);

            if (isCurrentUserContractRenter == false)
                AddNotification("AuthenticatedUserId", "Current user is not the contract tenant");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "User is authorized", null);
        }

        public ICommandResult Handle(AuthRenterCommand command)
        {
            bool hasUserReachedLimitOfContracts = _contractService.HasUserReachedLimitOfContracts(command.RenterId);
            if (hasUserReachedLimitOfContracts == true)
                AddNotification("ContractUserProdile", "You have reached the limit of countracts as renter");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "Renter has not reached the limit of contracts allowed to his account", null);
        }
    }
}

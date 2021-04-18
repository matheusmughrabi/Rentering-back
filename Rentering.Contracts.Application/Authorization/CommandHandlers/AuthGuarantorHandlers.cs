using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Domain.Services;

namespace Rentering.Contracts.Application.Authorization.CommandHandlers
{
    public class AuthGuarantorHandlers : Notifiable,
        ICommandHandler<AuthCurrentUserAndProfileGuarantorMatchCommand>
    {
        private readonly IAuthGuarantorService _authGuarantorService;

        public AuthGuarantorHandlers(IAuthGuarantorService authGuarantorService)
        {
            _authGuarantorService = authGuarantorService;
        }

        public ICommandResult Handle(AuthCurrentUserAndProfileGuarantorMatchCommand command)
        {
            bool isCurrentUserContractGuarantor = _authGuarantorService.IsCurrentUserGuarantorProfileOwner(command.AccountId, command.GuarantorId);

            if (isCurrentUserContractGuarantor == false)
                AddNotification("AuthenticatedUserId", "Current user does not own this guarantor profile.");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "User is authorized", null);
        }
    }
}

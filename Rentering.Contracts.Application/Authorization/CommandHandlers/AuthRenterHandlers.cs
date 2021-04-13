using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Domain.Services;
using System;

namespace Rentering.Contracts.Application.Authorization.CommandHandlers
{
    public class AuthRenterHandlers : Notifiable,
        ICommandHandler<AuthCurrentUserAndProfileRenterMatchCommand>
    {
        private readonly IAuthRenterService _authRenterService;

        public AuthRenterHandlers(IAuthRenterService authRenterService)
        {
            _authRenterService = authRenterService;
        }

        public ICommandResult Handle(AuthCurrentUserAndProfileRenterMatchCommand command)
        {
            bool isCurrentUserContractRenter = _authRenterService.IsCurrentUserRenterProfileOwner(command.AccountId, command.RenterId);

            if (isCurrentUserContractRenter == false)
                AddNotification("AuthenticatedUserId", "Current user does not own this renter profile.");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "User is authorized", null);
        }
    }
}

using FluentValidator;
using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Application.Authorization.Commands;
using Rentering.Contracts.Domain.Services;

namespace Rentering.Contracts.Application.Authorization.CommandHandlers
{
    public class AuthTenantHandlers : Notifiable,
        ICommandHandler<AuthCurrentUserAndProfileTenantMatchCommand>
    {
        private readonly IAuthTenantService _authTenantService;

        public AuthTenantHandlers(IAuthTenantService authTenantService)
        {
            _authTenantService = authTenantService;
        }

        public ICommandResult Handle(AuthCurrentUserAndProfileTenantMatchCommand command)
        {
            bool isCurrentUserContractTenant = _authTenantService.IsCurrentUserTenantProfileOwner(command.AccountId, command.TenantId);

            if (isCurrentUserContractTenant == false)
                AddNotification("AuthenticatedUserId", "Current user does not own this tenant profile.");

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            return new CommandResult(true, "User is authorized", null);
        }
    }
}

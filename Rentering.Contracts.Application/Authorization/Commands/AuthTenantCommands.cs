using Rentering.Common.Shared.Commands;

namespace Rentering.Contracts.Application.Authorization.Commands
{
    public class AuthCurrentUserAndProfileTenantMatchCommand : ICommand
    {
        public AuthCurrentUserAndProfileTenantMatchCommand(int accountId, int tenantId)
        {
            AccountId = accountId;
            TenantId = tenantId;
        }

        public int AccountId { get; set; }
        public int TenantId { get; set; }
    }
}

namespace Rentering.Contracts.Domain.Services
{
    public interface IAuthTenantService
    {
        bool IsCurrentUserTenantProfileOwner(int accountId, int tenantProfileId);
    }
}

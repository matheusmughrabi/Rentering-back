namespace Rentering.Contracts.Domain.Services
{
    public interface IAuthGuarantorService
    {
        bool IsCurrentUserGuarantorProfileOwner(int accountId, int tenantProfileId);
    }
}

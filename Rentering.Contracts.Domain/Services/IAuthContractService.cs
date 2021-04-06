namespace Rentering.Contracts.Domain.Services
{
    public interface IAuthContractService
    {
        bool HasUserReachedLimitOfContracts(int contractUserProfile);
        bool IsCurrentUserContractRenter(int accountId, int contractId);
        bool IsCurrentUserContractTenant(int accountId, int contractId);
    }
}

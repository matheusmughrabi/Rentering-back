namespace Rentering.Contracts.Domain.Services
{
    // TODO - OBSOLETE

    public interface IAuthContractService
    {
        bool HasUserReachedLimitOfContracts(int contractUserProfile);
        bool IsCurrentUserContractRenter(int accountId, int contractId);
        bool IsCurrentUserContractTenant(int accountId, int contractId);
    }
}

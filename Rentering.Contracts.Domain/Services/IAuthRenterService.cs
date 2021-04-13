namespace Rentering.Contracts.Domain.Services
{
    public interface IAuthRenterService
    {
        bool IsCurrentUserRenterProfileOwner(int accountId, int renterProfileId);
    }
}

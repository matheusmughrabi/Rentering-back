namespace Rentering.Contracts.Domain.Services
{
    public interface IAuthRenterService
    {
        bool IsCurrentUserTheOwnerOfRenterProfile(int accountId, int renterProfileId);
    }
}

using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthRenterService : IAuthRenterService
    {
        private readonly IRenterQueryRepository _renterQueryRepository;

        public AuthRenterService(IRenterQueryRepository renterQueryRepository)
        {
            _renterQueryRepository = renterQueryRepository;
        }

        public bool IsCurrentUserTheOwnerOfRenterProfile(int accountId, int renterProfileId)
        {
            var renterProfilesOfTheCurrentUser = _renterQueryRepository.GetRenterProfilesOfCurrentUser(accountId);

            bool containsPassedRenterProfileId = renterProfilesOfTheCurrentUser.ToList().Any(c => c.Id == renterProfileId);

            return containsPassedRenterProfileId;
        }
    }
}

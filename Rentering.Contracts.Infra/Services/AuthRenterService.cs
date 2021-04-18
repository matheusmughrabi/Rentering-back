using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthRenterService : IAuthRenterService
    {
        private readonly IRenterAuthRepository _authRenterRepository;

        public AuthRenterService(IRenterAuthRepository authRenterRepository)
        {
            _authRenterRepository = authRenterRepository;
        }

        public bool IsCurrentUserTheOwnerOfRenterProfile(int accountId, int renterProfileId)
        {
            var renterProfilesOfTheCurrentUser = _authRenterRepository.GetRenterProfilesOfTheCurrentUser(accountId);

            bool containsPassedRenterProfileId = renterProfilesOfTheCurrentUser.ToList().Any(c => c.Id == renterProfileId);

            return containsPassedRenterProfileId;
        }
    }
}

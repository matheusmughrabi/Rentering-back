using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthGuarantorService : IAuthGuarantorService
    {
        private readonly IGuarantorAuthRepository _authGuarantorRepository;

        public AuthGuarantorService(IGuarantorAuthRepository authGuarantorRepository)
        {
            _authGuarantorRepository = authGuarantorRepository;
        }

        public bool IsCurrentUserGuarantorProfileOwner(int accountId, int guarantorProfileId)
        {
            var guarantorProfilesOfTheCurrentUser = _authGuarantorRepository.GetGuarantorProfilesOfTheCurrentUser(accountId);

            bool containsPassedGuarantorProfileId = guarantorProfilesOfTheCurrentUser.ToList().Any(c => c.Id == guarantorProfileId);

            return containsPassedGuarantorProfileId;
        }
    }
}

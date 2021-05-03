using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthGuarantorService : IAuthGuarantorService
    {
        private readonly IGuarantorQueryRepository _guarantorQueryRepository;

        public AuthGuarantorService(IGuarantorQueryRepository guarantorQueryRepository)
        {
            _guarantorQueryRepository = guarantorQueryRepository;
        }

        public bool IsCurrentUserGuarantorProfileOwner(int accountId, int guarantorProfileId)
        {
            var guarantorProfilesOfTheCurrentUser = _guarantorQueryRepository.GetGuarantorProfilesOfCurrentUser(accountId);

            bool containsPassedGuarantorProfileId = guarantorProfilesOfTheCurrentUser.ToList().Any(c => c.Id == guarantorProfileId);

            return containsPassedGuarantorProfileId;
        }
    }
}

using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthTenantService : IAuthTenantService
    {
        private readonly ITenantQueryRepository _tenantQueryRepository;

        public AuthTenantService(ITenantQueryRepository tenantQueryRepository)
        {
            _tenantQueryRepository = tenantQueryRepository;
        }

        public bool IsCurrentUserTenantProfileOwner(int accountId, int tenantProfileId)
        {
            var tenantProfilesOfTheCurrentUser = _tenantQueryRepository.GetTenantProfilesOfCurrentUser(accountId);

            bool containsPassedTenantProfileId = tenantProfilesOfTheCurrentUser.ToList().Any(c => c.Id == tenantProfileId);

            return containsPassedTenantProfileId;
        }
    }
}

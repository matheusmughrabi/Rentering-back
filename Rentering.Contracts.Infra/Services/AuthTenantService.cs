using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Services;
using System;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthTenantService : IAuthTenantService
    {
        private readonly ITenantAuthRepository _authTenantRepository;

        public AuthTenantService(ITenantAuthRepository authTenantRepository)
        {
            _authTenantRepository = authTenantRepository;
        }

        public bool IsCurrentUserTenantProfileOwner(int accountId, int tenantProfileId)
        {
            var tenantProfilesOfTheCurrentUser = _authTenantRepository.GetTenantProfilesOfTheCurrentUser(accountId);

            bool containsPassedTenantProfileId = tenantProfilesOfTheCurrentUser.ToList().Any(c => c.Id == tenantProfileId);

            return containsPassedTenantProfileId;
        }
    }
}

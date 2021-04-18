using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Services;
using System;

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
            throw new NotImplementedException();
        }
    }
}

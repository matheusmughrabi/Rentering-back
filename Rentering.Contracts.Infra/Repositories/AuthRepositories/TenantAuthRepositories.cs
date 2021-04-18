using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Infra.Repositories.AuthRepositories
{
    public class TenantAuthRepositories : ITenantAuthRepository
    {
        public IEnumerable<AuthTenantProfilesOfTheCurrentUserQueryResults> GetTenantProfilesOfTheCurrentUser(int accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}

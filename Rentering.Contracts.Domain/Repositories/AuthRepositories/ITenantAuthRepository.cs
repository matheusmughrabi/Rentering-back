using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.AuthRepositories
{
    public interface ITenantAuthRepository
    {
        IEnumerable<AuthTenantProfilesOfTheCurrentUserQueryResults> GetTenantProfilesOfTheCurrentUser(int accountId);
    }
}

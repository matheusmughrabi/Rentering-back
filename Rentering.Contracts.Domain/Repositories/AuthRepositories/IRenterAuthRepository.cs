using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.AuthRepositories
{
    public interface IRenterAuthRepository
    {
        IEnumerable<AuthRenterProfilesOfTheCurrentUserQueryResults> GetRenterProfilesOfTheCurrentUser(int accountId);
    }
}

using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.AuthRepositories
{
    public interface IGuarantorAuthRepository
    {
        IEnumerable<AuthGuarantorProfilesOfTheCurrentUserQueryResults> GetGuarantorProfilesOfTheCurrentUser(int accountId);
    }
}

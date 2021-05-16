using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using System;
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
            throw new NotImplementedException();

            //var guarantorProfilesOfTheCurrentUser = _guarantorQueryRepository.GetGuarantorProfilesOfCurrentUser(accountId);

            //bool containsPassedGuarantorProfileId = guarantorProfilesOfTheCurrentUser.ToList().Any(c => c.Id == guarantorProfileId);

            //return containsPassedGuarantorProfileId;
        }
    }
}

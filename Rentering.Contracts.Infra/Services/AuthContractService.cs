using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Services;
using System.Linq;

namespace Rentering.Contracts.Infra.Services
{
    public class AuthContractService : IAuthContractService
    {
        private const int _limitOfContractsPerContractUserProfile = 2;
        private IContractQueryRepository _contractQueryContract;
        private readonly IContractAuthRepository _contractAuthRepository;

        public AuthContractService(IContractQueryRepository contractQueryContract, IContractAuthRepository contractAuthRepository)
        {
            _contractQueryContract = contractQueryContract;
            _contractAuthRepository = contractAuthRepository;
        }

        public bool IsCurrentUserContractRenter(int accountId, int contractId)
        {
            var contractAuthQueryResult = _contractAuthRepository.GetContractUserProfileIdOfTheCurrentUser(accountId);
            var contractProfileUserId = contractAuthQueryResult.Id;

            var authContracParticipants = _contractAuthRepository.GetContractParticipants(contractId);
            var contractRenterId = authContracParticipants.RenterId;

            if (contractProfileUserId != contractRenterId)
                return false;

            return true;
        }

        public bool IsCurrentUserContractTenant(int accountId, int contractId)
        {
            var contractAuthQueryResult = _contractAuthRepository.GetContractUserProfileIdOfTheCurrentUser(accountId);
            var contractProfileUserId = contractAuthQueryResult.Id;

            var authContracParticipants = _contractAuthRepository.GetContractParticipants(contractId);
            var contractTenantId = authContracParticipants.TenantId;

            if (contractProfileUserId != contractTenantId)
                return false;

            return true;
        }

        public bool HasUserReachedLimitOfContracts(int contractUserProfile)
        {
            var renterContracts = _contractQueryContract.GetContractsOfRenter(contractUserProfile);

            if (renterContracts.Count() >= _limitOfContractsPerContractUserProfile)
                return true;

            return false;
        }
    }
}

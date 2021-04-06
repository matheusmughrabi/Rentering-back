using Rentering.Contracts.Domain.Repositories.AuthRepositories.QueryResults;

namespace Rentering.Contracts.Domain.Repositories.AuthRepositories
{
    public interface IContractAuthRepository
    {
        AuthContractUserProfilesQueryResult GetContractUserProfileIdOfTheCurrentUser(int accountId);
        AuthContracParticipantsQueryResult GetContractParticipants(int contractId);
    }
}

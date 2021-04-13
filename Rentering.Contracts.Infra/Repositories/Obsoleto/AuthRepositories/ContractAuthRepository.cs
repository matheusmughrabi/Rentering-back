using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.QueryResults;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.AuthRepositories
{
    public class ContractAuthRepository : IContractAuthRepository
    {
        private readonly RenteringDataContext _context;

        public ContractAuthRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public AuthContractUserProfilesQueryResult GetContractUserProfileIdOfTheCurrentUser(int accountId)
        {
            var contractAuthQueryResults = _context.Connection.Query<AuthContractUserProfilesQueryResult>(
                    "SELECT [Id], [AccountId] FROM [dbo].[ContractUserProfiles] where AccountId = @AccountId",
                    new { AccountId = accountId })
                .FirstOrDefault();

            return contractAuthQueryResults;
        }

        public AuthContracParticipantsQueryResult GetContractParticipants(int contractId)
        {
            var contractAuthQueryResults = _context.Connection.Query<AuthContracParticipantsQueryResult>(
                    "SELECT [Id], [RenterId], [TenantId] FROM [dbo].[Contracts] where Id = @Id",
                    new { Id = contractId })
                .FirstOrDefault();

            return contractAuthQueryResults;
        }
    }
}

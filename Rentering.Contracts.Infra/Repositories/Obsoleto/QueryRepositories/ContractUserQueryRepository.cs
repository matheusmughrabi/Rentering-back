using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Application.QueryResults;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
{
    public class ContractUserQueryRepository : IContractUserProfileQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractUserQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GetContractUserProfileQueryResult GetUserById(int id)
        {
            var userQueryResult = _context.Connection.Query<GetContractUserProfileQueryResult>(
                    "SELECT AccountId FROM ContractUserProfiles where Id = @Id",
                    new { Id = id }
                ).FirstOrDefault();

            return userQueryResult;
        }

        public IEnumerable<GetContractUserProfileQueryResult> GetCurrentUserProfiles(int accountId)
        {
            var userQueryResult = _context.Connection.Query<GetContractUserProfileQueryResult>(
                    "SELECT [Id], [AccountId] FROM ContractUserProfiles where AccountId = @AccountId",
                    new { AccountId = accountId });

            return userQueryResult;
        }
    }
}

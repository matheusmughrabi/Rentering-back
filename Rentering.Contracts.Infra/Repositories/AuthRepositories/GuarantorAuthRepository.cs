using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Repositories.AuthRepositories;
using Rentering.Contracts.Domain.Repositories.AuthRepositories.AuthQueryResults;
using System.Collections.Generic;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.AuthRepositories
{
    public class GuarantorAuthRepository : IGuarantorAuthRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorAuthRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<AuthGuarantorProfilesOfTheCurrentUserQueryResults> GetGuarantorProfilesOfTheCurrentUser(int accountId)
        {
            var guarantorsFromDb = _context.Connection.Query<AuthGuarantorProfilesOfTheCurrentUserQueryResults>(
                    "sp_Guarantors_Auth_GetGuarantorsIdsOfAccount",
                    new { AccountId = accountId },
                    commandType: CommandType.StoredProcedure
                );

            return guarantorsFromDb;
        }
    }
}

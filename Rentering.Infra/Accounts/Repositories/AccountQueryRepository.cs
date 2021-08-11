using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Data.Repositories;
using Rentering.Accounts.Domain.Data.Repositories.QueryResults;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.StaticResources;
using Rentering.Common.Shared.Enums;
using Rentering.Common.Shared.QueryResults;
using System.Linq;

namespace Rentering.Infra.Accounts.Repositories
{
    public class AccountQueryRepository : IAccountQueryRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public AccountQueryRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public GetAccountQueryResult GetAccountById(int id)
        {
            var accountEntity = _renteringDbContext.Account
                 .AsNoTracking()
                 .Where(c => c.Id == id)
                 .Select(c => new { c.Email.Email, c.Username.Username })
                 .FirstOrDefault();

            if (accountEntity == null)
                return null;

            var accountQueryResult = new GetAccountQueryResult()
            {
                Email = accountEntity.Email,
                Username = accountEntity.Username
            };

            return accountQueryResult;
        }

        public SingleQueryResult<GetLicenseDetailsQueryResult> GetLicenseDetails(int licenseCode)
        {
            var license = Licenses.LicensesList
                .Where(c => c.Code == licenseCode)
                .Select(p => new GetLicenseDetailsQueryResult() 
                {
                    Code = p.Code,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                })
                .FirstOrDefault();

            var queryResult = new SingleQueryResult<GetLicenseDetailsQueryResult>(license);

            return queryResult;
        }
    }
}

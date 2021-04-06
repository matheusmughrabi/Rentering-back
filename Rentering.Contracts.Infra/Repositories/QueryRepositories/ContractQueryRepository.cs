using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Application.QueryResults;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
{
    public class ContractQueryRepository : IContractQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GetContractQueryResult GetContractById(int id)
        {
            var contractQueryResult = _context.Connection.Query<GetContractQueryResult>(
                     "SELECT ContractName, RentPrice FROM Contracts WHERE Id = @Id",
                     new { Id = id }
                 ).FirstOrDefault();

            return contractQueryResult;
        }

        public IEnumerable<GetContractQueryResult> GetContractsOfRenter(int renterId)
        {
            var contractQueryResult = _context.Connection.Query<GetContractQueryResult>(
                      "SELECT ContractName, RentPrice FROM Contracts WHERE RenterId = @RenterId",
                      new { RenterId = renterId }
                  );

            return contractQueryResult;
        }

        public IEnumerable<GetContractQueryResult> GetContractsOfTenant(int tenantId)
        {
            var contractQueryResult = _context.Connection.Query<GetContractQueryResult>(
                      "SELECT ContractName, RentPrice FROM Contracts WHERE TenantId = @TenantId",
                      new { TenantId = tenantId }
                  );

            return contractQueryResult;
        }
    }
}

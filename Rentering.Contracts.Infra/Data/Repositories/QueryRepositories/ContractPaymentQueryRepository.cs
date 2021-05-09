using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class ContractPaymentQueryRepository : IContractPaymentQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractPaymentQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<GetContractPaymentQueryResult> GetAll()
        {
            var paymentsFromDb = _context.Connection.Query<GetContractPaymentQueryResult>(
                    "sp_ContractPayments_Query_GetAllPayments",
                    commandType: CommandType.StoredProcedure
                );

            return paymentsFromDb;
        }

        public GetContractPaymentQueryResult GetById(int id)
        {
            var paymentFromDb = _context.Connection.Query<GetContractPaymentQueryResult>(
                    "sp_ContractPayments_Query_GetPaymentById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return paymentFromDb;
        }
    }
}

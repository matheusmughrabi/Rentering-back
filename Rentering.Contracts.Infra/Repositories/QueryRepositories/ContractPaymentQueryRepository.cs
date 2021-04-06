using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Application.QueryResults;
using Rentering.Contracts.Domain.Repositories.QueryRepositories;
using System.Collections.Generic;

namespace Rentering.Contracts.Infra.Repositories.QueryRepositories
{
    public class ContractPaymentQueryRepository : IContractPaymentQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractPaymentQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<GetContractPaymentsQueryResult> GetContractPayments(int contractId)
        {
            var contractPaymentQueryResults = _context.Connection.Query<GetContractPaymentsQueryResult>(
                    "SELECT ContractId, Month, RenterPaymentStatus, TenantPaymentStatus FROM ContractPayments where ContractId = @contractId",
                    new { ContractId = contractId }
                );

            return contractPaymentQueryResults;
        }
    }
}

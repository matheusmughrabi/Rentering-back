using Rentering.Contracts.Application.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractPaymentQueryRepository
    {
        IEnumerable<GetContractPaymentsQueryResult> GetContractPayments(int contractId);
    }
}

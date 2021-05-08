using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractPaymentQueryRepository
    {
        IEnumerable<GetContractPaymentQueryResult> GetAllPayments();
        GetContractPaymentQueryResult GetPaymentById(int id);
    }
}

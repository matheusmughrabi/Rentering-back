using Rentering.Contracts.Application.QueryResults;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractQueryRepository
    {
        GetContractQueryResult GetContractById(int id);
        IEnumerable<GetContractQueryResult> GetContractsOfRenter(int renterId);
        IEnumerable<GetContractQueryResult> GetContractsOfTenant(int tenantId);
    }
}

using Rentering.Corporation.Domain.Data.Repositories.QueryResults;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Data.Repositories
{
    public interface ICorporationQueryRepository
    {
        IEnumerable<GetCorporationsQueryResult> GetCorporations(int accountId);
    }
}

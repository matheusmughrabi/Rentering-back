using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories
{
    public interface ITenantCUDRepository : IGenericCUDRepository<TenantEntity>
    {
        TenantEntity GetTenantForCUD(int tenantId);
    }
}

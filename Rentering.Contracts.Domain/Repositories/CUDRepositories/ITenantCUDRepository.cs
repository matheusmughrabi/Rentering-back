using Rentering.Common.Shared.Repositories;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface ITenantCUDRepository : IGenericCUDRepository<TenantEntity>
    {
    }
}

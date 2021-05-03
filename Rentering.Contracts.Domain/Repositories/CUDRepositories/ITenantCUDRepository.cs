using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface ITenantCUDRepository
    {
        void CreateTenant(TenantEntity tenant);
        void UpdateTenant(int id, TenantEntity tenant);
        void DeleteTenant(int id);
    }
}

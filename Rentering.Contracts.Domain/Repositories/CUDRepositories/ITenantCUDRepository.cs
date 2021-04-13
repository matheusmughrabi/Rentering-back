using Rentering.Contracts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface ITenantCUDRepository
    {
        bool CheckIfAccountExists(int accountId);
        void CreateTenant(TenantEntity tenant);
    }
}

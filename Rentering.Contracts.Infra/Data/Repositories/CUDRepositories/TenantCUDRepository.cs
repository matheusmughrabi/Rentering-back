using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Data;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class TenantCUDRepository : ITenantCUDRepository
    {
        private readonly RenteringDataContext _context;

        public TenantCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void Create(TenantEntity tenant)
        {
            _context.Connection.Execute("sp_Tenants_CUD_CreateTenant",
                    new
                    {
                        tenant.AccountId,
                        Status = tenant.TenantStatus,
                        tenant.Name.FirstName,
                        tenant.Name.LastName,
                        tenant.Nationality,
                        tenant.Ocupation,
                        tenant.MaritalStatus,
                        tenant.IdentityRG.IdentityRG,
                        tenant.CPF.CPF,
                        tenant.Address.Street,
                        tenant.Address.Neighborhood,
                        tenant.Address.City,
                        tenant.Address.CEP,
                        tenant.Address.State,
                        SpouseFirstName = tenant.SpouseName.FirstName,
                        SpouseLastName = tenant.SpouseName.LastName,
                        tenant.SpouseNationality,
                        tenant.SpouseOcupation,
                        SpouseIdentityRG = tenant.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = tenant.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Update(int id, TenantEntity tenant)
        {
            _context.Connection.Execute("sp_Tenants_CUD_UpdateTenant",
                    new
                    {
                        Id = id,
                        tenant.AccountId,
                        Status = tenant.TenantStatus,
                        tenant.Name.FirstName,
                        tenant.Name.LastName,
                        tenant.Nationality,
                        tenant.Ocupation,
                        tenant.MaritalStatus,
                        tenant.IdentityRG.IdentityRG,
                        tenant.CPF.CPF,
                        tenant.Address.Street,
                        tenant.Address.Neighborhood,
                        tenant.Address.City,
                        tenant.Address.CEP,
                        tenant.Address.State,
                        SpouseFirstName = tenant.SpouseName.FirstName,
                        SpouseLastName = tenant.SpouseName.LastName,
                        tenant.SpouseNationality,
                        tenant.SpouseOcupation,
                        SpouseIdentityRG = tenant.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = tenant.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Delete(int id)
        {
            _context.Connection.Execute("sp_Tenants_CUD_DeleteTenant",
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
    }
}

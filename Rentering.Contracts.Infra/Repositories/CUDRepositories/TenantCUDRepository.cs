using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class TenantCUDRepository : ITenantCUDRepository
    {
        private readonly RenteringDataContext _context;

        public TenantCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfAccountExists(int accountId)
        {
            var accountExists = _context.Connection.Query<bool>(
                    "sp_Accounts_Util_CheckIfAccountExists",
                    new { Id = accountId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return accountExists;
        }

        public void CreateTenant(TenantEntity tenant)
        {
            _context.Connection.Execute("sp_Tenants_CUD_CreateTenant",
                    new
                    {
                        AccountId = tenant.AccountId,
                        FirstName = tenant.Name.FirstName,
                        LastName = tenant.Name.LastName,
                        Nationality = tenant.Nationality,
                        Ocupation = tenant.Ocupation,
                        MaritalStatus = tenant.MaritalStatus,
                        IdentityRG = tenant.IdentityRG.IdentityRG,
                        CPF = tenant.CPF.CPF,
                        Street = tenant.Address.Street,
                        Neighborhood = tenant.Address.Neighborhood,
                        City = tenant.Address.City,
                        CEP = tenant.Address.CEP,
                        State = tenant.Address.State,
                        SpouseFirstName = tenant.SpouseName.FirstName,
                        SpouseLastName = tenant.SpouseName.LastName,
                        SpouseNationality = tenant.SpouseNationality,
                        SpouseOcupation = tenant.SpouseOcupation,
                        SpouseIdentityRG = tenant.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = tenant.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void UpdateTenant(int id, TenantEntity tenant)
        {
            _context.Connection.Execute("sp_Tenants_CUD_UpdateTenant",
                    new
                    {
                        Id = id,
                        AccountId = tenant.AccountId,
                        FirstName = tenant.Name.FirstName,
                        LastName = tenant.Name.LastName,
                        Nationality = tenant.Nationality,
                        Ocupation = tenant.Ocupation,
                        MaritalStatus = tenant.MaritalStatus,
                        IdentityRG = tenant.IdentityRG.IdentityRG,
                        CPF = tenant.CPF.CPF,
                        Street = tenant.Address.Street,
                        Neighborhood = tenant.Address.Neighborhood,
                        City = tenant.Address.City,
                        CEP = tenant.Address.CEP,
                        State = tenant.Address.State,
                        SpouseFirstName = tenant.SpouseName.FirstName,
                        SpouseLastName = tenant.SpouseName.LastName,
                        SpouseNationality = tenant.SpouseNationality,
                        SpouseOcupation = tenant.SpouseOcupation,
                        SpouseIdentityRG = tenant.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = tenant.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteTenant(int id)
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

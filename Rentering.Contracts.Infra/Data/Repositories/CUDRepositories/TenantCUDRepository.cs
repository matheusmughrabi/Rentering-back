﻿using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class TenantCUDRepository : ITenantCUDRepository
    {
        private readonly RenteringDataContext _context;

        public TenantCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public TenantEntity GetTenantForCUD(int tenantId)
        {
            var tenantSql = @"SELECT * FROM Tenants WHERE Id = @Id";

            var tenantFromDb = _context.Connection.Query<GetTenantForCUD>(
                   tenantSql,
                   new { Id = tenantId })
                .FirstOrDefault();

            if (tenantFromDb == null)
                return null;

            var tenantEntity = tenantFromDb.EntityFromModel();

            return tenantEntity;
        }

        public TenantEntity Create(TenantEntity tenant)
        {
            if (tenant == null)
                return null;

            var sql = @"INSERT INTO [Tenants] (
								[ContractId],
								[Status],
								[FirstName], 
								[LastName],
								[Nationality],
								[Ocupation],
								[MaritalStatus],
								[IdentityRG],
								[CPF],
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[SpouseFirstName],
								[SpouseLastName],
								[SpouseNationality],
								[SpouseOcupation],
								[SpouseIdentityRG],
								[SpouseCPF]) 
                        OUTPUT INSERTED.*
                        VALUES (
								@ContractId,
								@Status,
								@FirstName,
								@LastName,
								@Nationality,
								@Ocupation,
								@MaritalStatus,
								@IdentityRG,
								@CPF,
								@Street,
								@Neighborhood,
								@City,
								@CEP,
								@State,
								@SpouseFirstName,
								@SpouseLastName,
								@SpouseNationality,
								@SpouseOcupation,
								@SpouseIdentityRG,
								@SpouseCPF
							);";

            var tenantFromDb = _context.Connection.QuerySingle<GetTenantForCUD>(sql,
                    new
                    {
                        tenant.ContractId,
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
                    });

            var tenantEntity = tenantFromDb.EntityFromModel();
            return tenantEntity;
        }

        public TenantEntity Update(int id, TenantEntity tenant)
        {
            if (tenant == null)
                return null;

            var sql = @"UPDATE 
							Tenants
						SET
							[ContractId] = @ContractId,
							[Status] = @Status,
							[FirstName] = @FirstName, 
							[LastName] = @LastName,
							[Nationality] = @Nationality,
							[Ocupation] = @Ocupation,
							[MaritalStatus] = @MaritalStatus,
							[IdentityRG] = @IdentityRG,
							[CPF] = @CPF,
							[Street] = @Street,
							[Neighborhood] = @Neighborhood,
							[City] = @City,
							[CEP] = @CEP,
							[State] = @State,
							[SpouseFirstName] = @SpouseFirstName,
							[SpouseLastName] = @SpouseLastName,
							[SpouseNationality] = @SpouseNationality,
							[SpouseOcupation] = @SpouseOcupation,
							[SpouseIdentityRG] = @SpouseIdentityRG,
							[SpouseCPF] = @SpouseCPF
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var tenantFromDb = _context.Connection.QuerySingle<GetTenantForCUD>(sql,
                    new
                    {
                        Id = id,
                        tenant.ContractId,
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
                    });

            var tenantEntity = tenantFromDb.EntityFromModel();
            return tenantEntity;
        }

        public TenantEntity Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							Tenants
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var tenantFromDb = _context.Connection.QuerySingle<GetTenantForCUD>(sql,
                    new
                    {
                        Id = id
                    });

            var tenantEntity = tenantFromDb.EntityFromModel();
            return tenantEntity;
        }
    }
}
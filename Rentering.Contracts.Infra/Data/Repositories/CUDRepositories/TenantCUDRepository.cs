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
            var sql = @"INSERT INTO [Tenants] (
								[AccountId],
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
								[SpouseCPF]
							) VALUES (
								@AccountId,
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

            _context.Connection.Execute(sql,
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
                    });
        }

        public void Update(int id, TenantEntity tenant)
        {
            var sql = @"UPDATE 
							Tenants
						SET
							[AccountId] = @AccountId,
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
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
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
                    });
        }

        public void Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							Tenants
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
                    new
                    {
                        Id = id
                    });
        }
    }
}

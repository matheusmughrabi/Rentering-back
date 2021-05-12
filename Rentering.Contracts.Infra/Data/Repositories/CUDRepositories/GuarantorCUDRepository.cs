using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Extensions;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class GuarantorCUDRepository : IGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public GuarantorEntity GetGuarantorForCUD(int guarantorId)
        {
            var guarantorSql = @"SELECT * FROM Guarantors WHERE Id = @Id";

            var guarantorFromDb = _context.Connection.Query<GetGuarantorForCUD>(
                   guarantorSql,
                   new { Id = guarantorId })
                .FirstOrDefault();

            if (guarantorFromDb == null)
                return null;

            var guarantorEntity = guarantorFromDb.EntityFromModel();

            return guarantorEntity;
        }

        public void Create(GuarantorEntity guarantor)
        {
            var sql = @"INSERT INTO [Guarantors] (
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
                        guarantor.AccountId,
                        Status = guarantor.GuarantorStatus,
                        guarantor.Name.FirstName,
                        guarantor.Name.LastName,
                        guarantor.Nationality,
                        guarantor.Ocupation,
                        guarantor.MaritalStatus,
                        guarantor.IdentityRG.IdentityRG,
                        guarantor.CPF.CPF,
                        guarantor.Address.Street,
                        guarantor.Address.Neighborhood,
                        guarantor.Address.City,
                        guarantor.Address.CEP,
                        guarantor.Address.State,
                        SpouseFirstName = guarantor.SpouseName.FirstName,
                        SpouseLastName = guarantor.SpouseName.LastName,
                        guarantor.SpouseNationality,
                        guarantor.SpouseOcupation,
                        SpouseIdentityRG = guarantor.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = guarantor.SpouseCPF.CPF
                    });
        }

        public void Update(int id, GuarantorEntity guarantor)
        {
            var sql = @"UPDATE 
								Guarantors
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
                       guarantor.AccountId,
                       Status = guarantor.GuarantorStatus,
                       guarantor.Name.FirstName,
                       guarantor.Name.LastName,
                       guarantor.Nationality,
                       guarantor.Ocupation,
                       guarantor.MaritalStatus,
                       guarantor.IdentityRG.IdentityRG,
                       guarantor.CPF.CPF,
                       guarantor.Address.Street,
                       guarantor.Address.Neighborhood,
                       guarantor.Address.City,
                       guarantor.Address.CEP,
                       guarantor.Address.State,
                       SpouseFirstName = guarantor.SpouseName.FirstName,
                       SpouseLastName = guarantor.SpouseName.LastName,
                       guarantor.SpouseNationality,
                       guarantor.SpouseOcupation,
                       SpouseIdentityRG = guarantor.SpouseIdentityRG.IdentityRG,
                       SpouseCPF = guarantor.SpouseCPF.CPF
                   });
        }

        public void Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							Guarantors
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

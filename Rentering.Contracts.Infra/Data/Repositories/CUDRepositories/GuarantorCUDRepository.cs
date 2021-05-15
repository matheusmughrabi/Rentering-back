using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;
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

        public GuarantorEntity Create(GuarantorEntity guarantor)
        {
            if (guarantor == null)
                return null;

            var sql = @"INSERT INTO [Guarantors] (
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

            var createdGuarantorFromDb = _context.Connection.QuerySingle<GetGuarantorForCUD>(sql,
                    new
                    {
                        guarantor.ContractId,
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

            var createdGuarantorEntity = createdGuarantorFromDb.EntityFromModel();
            return createdGuarantorEntity;
        }

        public GuarantorEntity Update(int id, GuarantorEntity guarantor)
        {
            if (guarantor == null)
                return null;

            var sql = @"UPDATE 
								Guarantors
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

            var updatedGuarantorFromDb = _context.Connection.QuerySingle<GetGuarantorForCUD>(sql,
                   new
                   {
                       Id = id,
                       guarantor.ContractId,
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

            var updatedGuarantorEntity = updatedGuarantorFromDb.EntityFromModel();
            return updatedGuarantorEntity;
        }

        public GuarantorEntity Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							Guarantors
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var deletedGuarantorFromDb = _context.Connection.QuerySingle<GetGuarantorForCUD>(sql,
                    new
                    {
                        Id = id
                    });

            var deletedGuarantorEntity = deletedGuarantorFromDb.EntityFromModel();
            return deletedGuarantorEntity;
        }
    }
}

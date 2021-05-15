using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class RenterCUDRepository : IRenterCUDRepository
    {
        private readonly RenteringDataContext _context;

        public RenterCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public RenterEntity GetRenterForCUD(int renterId)
        {
            var renterSql = @"SELECT * FROM Renters WHERE Id = @Id";

            var renterFromDb = _context.Connection.Query<GetRenterForCUD>(
                   renterSql,
                   new { Id = renterId })
                .FirstOrDefault();

            if (renterFromDb == null)
                return null;

            var renterEntity = renterFromDb.EntityFromModel();

            return renterEntity;
        }

        public RenterEntity Create(RenterEntity renter)
        {
            var sql = @"INSERT INTO [Renters] (
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
								@SpouseIdentityRG,
								@SpouseCPF
							);";

            var renterFromDb = _context.Connection.QuerySingle<GetRenterForCUD>(sql,
                    new
                    {
                        renter.ContractId,
                        Status = renter.RenterStatus,
                        renter.Name.FirstName,
                        renter.Name.LastName,
                        renter.Nationality,
                        renter.Ocupation,
                        renter.MaritalStatus,
                        renter.IdentityRG.IdentityRG,
                        renter.CPF.CPF,
                        renter.Address.Street,
                        renter.Address.Neighborhood,
                        renter.Address.City,
                        renter.Address.CEP,
                        renter.Address.State,
                        SpouseFirstName = renter.SpouseName.FirstName,
                        SpouseLastName = renter.SpouseName.LastName,
                        renter.SpouseNationality,
                        SpouseIdentityRG = renter.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = renter.SpouseCPF.CPF
                    },
                    _context.Transaction);

            var renterEntity = renterFromDb.EntityFromModel();
            return renterEntity;
        }

        public RenterEntity Update(int id, RenterEntity renter)
        {
            var sql = @"UPDATE 
								Renters
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
								[SpouseIdentityRG] = @SpouseIdentityRG,
								[SpouseCPF] = @SpouseCPF
                            OUTPUT INSERTED.*
							WHERE 
								Id = @Id;";

            var renterFromDb = _context.Connection.QuerySingle<GetRenterForCUD>(sql,
                    new
                    {
                        Id = id,
                        renter.ContractId,
                        Status = renter.RenterStatus,
                        renter.Name.FirstName,
                        renter.Name.LastName,
                        renter.Nationality,
                        renter.Ocupation,
                        renter.MaritalStatus,
                        renter.IdentityRG.IdentityRG,
                        renter.CPF.CPF,
                        renter.Address.Street,
                        renter.Address.Neighborhood,
                        renter.Address.City,
                        renter.Address.CEP,
                        renter.Address.State,
                        SpouseFirstName = renter.SpouseName.FirstName,
                        SpouseLastName = renter.SpouseName.LastName,
                        renter.SpouseNationality,
                        SpouseIdentityRG = renter.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = renter.SpouseCPF.CPF
                    },
                    _context.Transaction);

            var renterEntity = renterFromDb.EntityFromModel();
            return renterEntity;
        }

        public RenterEntity Delete(int renterId)
        {
            var sql = @"DELETE 
                        FROM 
							Renters
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var renterFromDb = _context.Connection.QuerySingle<GetRenterForCUD>(sql,
                    new
                    {
                        Id = renterId
                    },
                    _context.Transaction);

            var renterEntity = renterFromDb.EntityFromModel();
            return renterEntity;
        }
    }
}

using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Extensions;
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

        public void Create(RenterEntity renter)
        {
            var sql = @"INSERT INTO [Renters] (
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
								@SpouseIdentityRG,
								@SpouseCPF
							);";

            _context.Connection.Execute(sql,
                    new
                    {
                        renter.AccountId,
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
        }

        public void Update(int id, RenterEntity renter)
        {
            var sql = @"UPDATE 
								Renters
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
								[SpouseIdentityRG] = @SpouseIdentityRG,
								[SpouseCPF] = @SpouseCPF
							WHERE 
								Id = @Id;";

            _context.Connection.Execute(sql,
                    new
                    {
                        Id = id,
                        renter.AccountId,
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
        }

        public void Delete(int renterId)
        {
            var sql = @"DELETE FROM 
								Renters
							WHERE 
								Id = @Id;";

            _context.Connection.Execute(sql,
                    new
                    {
                        Id = renterId
                    },
                    _context.Transaction);
        }
    }
}

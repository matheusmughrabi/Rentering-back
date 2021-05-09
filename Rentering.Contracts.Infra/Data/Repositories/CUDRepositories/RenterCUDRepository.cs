using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Data;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class RenterCUDRepository : IRenterCUDRepository
    {
        private readonly RenteringDataContext _context;

        public RenterCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void Create(RenterEntity renter)
        {
            _context.Connection.Execute("sp_Renters_CUD_CreateRenter",
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
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Update(int id, RenterEntity renter)
        {
            _context.Connection.Execute("sp_Renters_CUD_UpdateRenter",
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
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Delete(int renterId)
        {
            _context.Connection.Execute("sp_Renters_CUD_DeleteRenter",
                    new
                    {
                        Id = renterId
                    },
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }
    }
}

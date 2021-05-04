using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class RenterCUDRepository : IRenterCUDRepository
    {
        private readonly RenteringDataContext _context;

        public RenterCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void CreateRenter(RenterEntity renter)
        {
            _context.Connection.Execute("sp_Renters_CUD_CreateRenter",
                    new
                    {
                        AccountId = renter.AccountId,
                        Status = renter.RenterStatus,
                        FirstName = renter.Name.FirstName,
                        LastName = renter.Name.LastName,
                        Nationality = renter.Nationality,
                        Ocupation = renter.Ocupation,
                        MaritalStatus = renter.MaritalStatus,
                        IdentityRG = renter.IdentityRG.IdentityRG,
                        CPF = renter.CPF.CPF,
                        Street = renter.Address.Street,
                        Neighborhood = renter.Address.Neighborhood,
                        City = renter.Address.City,
                        CEP = renter.Address.CEP,
                        State = renter.Address.State,
                        SpouseFirstName = renter.SpouseName.FirstName,
                        SpouseLastName = renter.SpouseName.LastName,
                        SpouseNationality = renter.SpouseNationality,
                        SpouseIdentityRG = renter.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = renter.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void UpdateRenter(int id, RenterEntity renter)
        {
            _context.Connection.Execute("sp_Renters_CUD_UpdateRenter",
                    new
                    {
                        Id = id,
                        AccountId = renter.AccountId,
                        Status = renter.RenterStatus,
                        FirstName = renter.Name.FirstName,
                        LastName = renter.Name.LastName,
                        Nationality = renter.Nationality,
                        Ocupation = renter.Ocupation,
                        MaritalStatus = renter.MaritalStatus,
                        IdentityRG = renter.IdentityRG.IdentityRG,
                        CPF = renter.CPF.CPF,
                        Street = renter.Address.Street,
                        Neighborhood = renter.Address.Neighborhood,
                        City = renter.Address.City,
                        CEP = renter.Address.CEP,
                        State = renter.Address.State,
                        SpouseFirstName = renter.SpouseName.FirstName,
                        SpouseLastName = renter.SpouseName.LastName,
                        SpouseNationality = renter.SpouseNationality,
                        SpouseIdentityRG = renter.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = renter.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteRenter(int renterId)
        {
            _context.Connection.Execute("sp_Renters_CUD_DeleteRenter",
                    new 
                    { 
                        Id = renterId
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
    }
}

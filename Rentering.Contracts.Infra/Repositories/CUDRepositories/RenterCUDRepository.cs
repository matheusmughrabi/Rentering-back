using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class RenterCUDRepository : IRenterCUDRepository
    {
        private readonly RenteringDataContext _context;

        public RenterCUDRepository(RenteringDataContext context)
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

        public void CreateRenter(RenterEntity renter)
        {
            _context.Connection.Execute("sp_Renters_CUD_CreateRenter",
                    new
                    {
                        AccountId = renter.AccountId,
                        FirstName = renter.Name.FirstName,
                        LastName = renter.Name.LastName,
                        Nationality = renter.Nationality,
                        Ocupation = renter.Ocupation,
                        MaritalStatus = renter.MaritalStatus,
                        IdentityRG = renter.IdentityRG.IdentityRG,
                        CPF = renter.CPF.CPF,
                        Street = renter.Address.Street,
                        Bairro = renter.Address.Bairro,
                        Cidade = renter.Address.Cidade,
                        CEP = renter.Address.CEP,
                        Estado = renter.Address.Estado,
                        SpouseFirstName = renter.SpouseFirstName.FirstName,
                        SpouseLastName = renter.SpouseFirstName.LastName,
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
                        FirstName = renter.Name.FirstName,
                        LastName = renter.Name.LastName,
                        Nationality = renter.Nationality,
                        Ocupation = renter.Ocupation,
                        MaritalStatus = renter.MaritalStatus,
                        IdentityRG = renter.IdentityRG.IdentityRG,
                        CPF = renter.CPF.CPF,
                        Street = renter.Address.Street,
                        Bairro = renter.Address.Bairro,
                        Cidade = renter.Address.Cidade,
                        CEP = renter.Address.CEP,
                        Estado = renter.Address.Estado,
                        SpouseFirstName = renter.SpouseFirstName.FirstName,
                        SpouseLastName = renter.SpouseFirstName.LastName,
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

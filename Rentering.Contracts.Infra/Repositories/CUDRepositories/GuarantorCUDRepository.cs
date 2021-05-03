using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class GuarantorCUDRepository : IGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void CreateGuarantor(GuarantorEntity guarantor)
        {
            _context.Connection.Execute("sp_Guarantors_CUD_CreateGuarantor",
                    new
                    {
                        AccountId = guarantor.AccountId,
                        FirstName = guarantor.Name.FirstName,
                        LastName = guarantor.Name.LastName,
                        Nationality = guarantor.Nationality,
                        Ocupation = guarantor.Ocupation,
                        MaritalStatus = guarantor.MaritalStatus,
                        IdentityRG = guarantor.IdentityRG.IdentityRG,
                        CPF = guarantor.CPF.CPF,
                        Street = guarantor.Address.Street,
                        Neighborhood = guarantor.Address.Neighborhood,
                        City = guarantor.Address.City,
                        CEP = guarantor.Address.CEP,
                        State = guarantor.Address.State,
                        SpouseFirstName = guarantor.SpouseName.FirstName,
                        SpouseLastName = guarantor.SpouseName.LastName,
                        SpouseNationality = guarantor.SpouseNationality,
                        SpouseOcupation = guarantor.SpouseOcupation,
                        SpouseIdentityRG = guarantor.SpouseIdentityRG.IdentityRG,
                        SpouseCPF = guarantor.SpouseCPF.CPF
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void UpdateGuarantor(int id, GuarantorEntity guarantor)
        {
            _context.Connection.Execute("sp_Guarantors_CUD_UpdateGuarantor",
                   new
                   {
                       Id = id,
                       AccountId = guarantor.AccountId,
                       FirstName = guarantor.Name.FirstName,
                       LastName = guarantor.Name.LastName,
                       Nationality = guarantor.Nationality,
                       Ocupation = guarantor.Ocupation,
                       MaritalStatus = guarantor.MaritalStatus,
                       IdentityRG = guarantor.IdentityRG.IdentityRG,
                       CPF = guarantor.CPF.CPF,
                       Street = guarantor.Address.Street,
                       Neighborhood = guarantor.Address.Neighborhood,
                       City = guarantor.Address.City,
                       CEP = guarantor.Address.CEP,
                       State = guarantor.Address.State,
                       SpouseFirstName = guarantor.SpouseName.FirstName,
                       SpouseLastName = guarantor.SpouseName.LastName,
                       SpouseNationality = guarantor.SpouseNationality,
                       SpouseOcupation = guarantor.SpouseOcupation,
                       SpouseIdentityRG = guarantor.SpouseIdentityRG.IdentityRG,
                       SpouseCPF = guarantor.SpouseCPF.CPF
                   },
                   commandType: CommandType.StoredProcedure
               );
        }

        public void DeleteGuarantor(int id)
        {
            _context.Connection.Execute("sp_Guarantors_CUD_DeleteGuarantor",
                    new
                    {
                        Id = id
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
    }
}

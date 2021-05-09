using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Data;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class GuarantorCUDRepository : IGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void Create(GuarantorEntity guarantor)
        {
            _context.Connection.Execute("sp_Guarantors_CUD_CreateGuarantor",
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
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Update(int id, GuarantorEntity guarantor)
        {
            _context.Connection.Execute("sp_Guarantors_CUD_UpdateGuarantor",
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
                   },
                   commandType: CommandType.StoredProcedure
               );
        }

        public void Delete(int id)
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

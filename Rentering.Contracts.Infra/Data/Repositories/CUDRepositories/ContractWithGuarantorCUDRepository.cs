using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System;
using System.Data;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class ContractWithGuarantorCUDRepository : IContractWithGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractWithGuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void Create(ContractWithGuarantorEntity contract)
        {
            _context.Connection.Execute("sp_ContractsWithGuarantor_CUD_CreateContract",
                    new
                    {
                        contract.ContractName,
                        contract.RenterId,
                        contract.TenantId,
                        contract.GuarantorId,
                        contract.Address.Street,
                        contract.Address.Neighborhood,
                        contract.Address.City,
                        contract.Address.CEP,
                        contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        contract.RentDueDate,
                        contract.ContractStartDate,
                        contract.ContractEndDate
                    },
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }
        public void Update(int id, ContractWithGuarantorEntity contract)
        {
            _context.Connection.Execute("sp_ContractsWithGuarantor_CUD_UpdateContract",
                    new
                    {
                        contract.Id,
                        contract.ContractName,
                        contract.RenterId,
                        contract.TenantId,
                        contract.GuarantorId,
                        contract.Address.Street,
                        contract.Address.Neighborhood,
                        contract.Address.City,
                        contract.Address.CEP,
                        contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        contract.RentDueDate,
                        contract.ContractStartDate,
                        contract.ContractEndDate
                    },
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}

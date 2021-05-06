using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class ContractWithGuarantorCUDRepository : IContractWithGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractWithGuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void CreateContract(ContractWithGuarantorEntity contract)
        {
            _context.Connection.Execute("sp_ContractsWithGuarantor_CUD_CreateContract",
                    new
                    {
                        ContractName = contract.ContractName,
                        RenterId = contract.RenterId,
                        TenantId = contract.TenantId,
                        GuarantorId = contract.GuarantorId,
                        Street = contract.Address.Street,
                        Neighborhood = contract.Address.Neighborhood,
                        City = contract.Address.City,
                        CEP = contract.Address.CEP,
                        State = contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        RentDueDate = contract.RentDueDate,
                        ContractStartDate = contract.ContractStartDate,
                        ContractEndDate = contract.ContractEndDate
                    },
                    commandType: CommandType.StoredProcedure
                );
        }
        public void UpdateContract(int id, ContractWithGuarantorEntity contract)
        {
            _context.Connection.Execute("sp_ContractsWithGuarantor_CUD_UpdateContract",
                    new
                    {
                        Id = contract.Id,
                        ContractName = contract.ContractName,
                        RenterId = contract.RenterId,
                        TenantId = contract.TenantId,
                        GuarantorId = contract.GuarantorId,
                        Street = contract.Address.Street,
                        Neighborhood = contract.Address.Neighborhood,
                        City = contract.Address.City,
                        CEP = contract.Address.CEP,
                        State = contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        RentDueDate = contract.RentDueDate,
                        ContractStartDate = contract.ContractStartDate,
                        ContractEndDate = contract.ContractEndDate
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteContract(int id)
        {
            throw new NotImplementedException();
        }

    }
}

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
            var sql = @"INSERT INTO [ContractsWithGuarantor] (
								[ContractName], 
								[RenterId],
								[TenantId],
								[GuarantorId],
								[Street],
								[Neighborhood],
								[City],
								[CEP],
								[State],
								[PropertyRegistrationNumber],
								[RentPrice],
								[RentDueDate],
								[ContractStartDate],
								[ContractEndDate]
							) VALUES (
								@ContractName,
								@RenterId,
								@TenantId,
								@GuarantorId,
								@Street,
								@Neighborhood,
								@City,
								@CEP,
								@State,
								@PropertyRegistrationNumber,
								@RentPrice,
								@RentDueDate,
								@ContractStartDate,
								@ContractEndDate
							);";

            _context.Connection.Execute(sql,
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
                    _context.Transaction);
        }
        public void Update(int id, ContractWithGuarantorEntity contract)
        {
            var sql = @"UPDATE 
							ContractsWithGuarantor
						SET
							[ContractName] = @ContractName,
							[RenterId] = @RenterId,
							[TenantId] = @TenantId,
							[GuarantorId] = @GuarantorId,
							[Street] = @Street,
							[Neighborhood] = @Neighborhood,
							[City] = @City,
							[CEP] = @CEP,
							[State] = @State,
							[PropertyRegistrationNumber] = @PropertyRegistrationNumber,
							[RentPrice] = @RentPrice,
							[RentDueDate] = @RentDueDate,
							[ContractStartDate] = @ContractStartDate,
							[ContractEndDate] = @ContractEndDate
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
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
                    _context.Transaction);
        }

        public void Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							ContractsWithGuarantor
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
                   new{ Id = id },
                   _context.Transaction);
        }
    }
}

using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Extensions;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class ContractWithGuarantorCUDRepository : IContractWithGuarantorCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractWithGuarantorCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public ContractWithGuarantorEntity GetContractForCUD(int id)
        {
            var contractSql = @"SELECT * FROM ContractsWithGuarantor WHERE Id = @Id";
            var paymentSql = @"SELECT * FROM ContractPayments WHERE ContractId = @ContractId";

            var contractFromDb = _context.Connection.Query<GetContractWithGuarantorForCUD>(
                   contractSql,
                   new { Id = id }).FirstOrDefault();

            if (contractFromDb == null)
                return null;

            var paymentsFromDb = _context.Connection.Query<GetPaymentForCUD>(
                   paymentSql,
                   new { ContractId = id });

            var contractEntity = contractFromDb.EntityFromModel();
            var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            contractEntity.IncludeContractPayments(paymentEntities);

            return contractEntity;
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

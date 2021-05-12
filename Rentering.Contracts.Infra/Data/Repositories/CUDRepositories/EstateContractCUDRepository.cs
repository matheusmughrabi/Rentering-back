using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class EstateContractCUDRepository : IEstateContractCUDRepository
    {
        private readonly RenteringDataContext _context;

        public EstateContractCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public EstateContractEntity GetContractForCUD(int id)
        {
            var contractSql = @"SELECT * FROM EstateContracts WHERE Id = @Id";
            var paymentSql = @"SELECT * FROM ContractPayments WHERE ContractId = @ContractId";

            var contractFromDb = _context.Connection.Query<GetEstateContractForCUD>(
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

        public void Create(EstateContractEntity contract)
        {
            var sql = @"INSERT INTO [EstateContracts] (
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
        public void Update(int id, EstateContractEntity contract)
        {
            var sql = @"UPDATE 
							EstateContracts
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
							EstateContracts
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
                   new{ Id = id },
                   _context.Transaction);
        }
    }
}

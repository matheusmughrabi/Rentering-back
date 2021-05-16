using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class ContractPaymentCUDRepository : IContractPaymentCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractPaymentCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public ContractPaymentEntity Create(ContractPaymentEntity payment)
        {
            if (payment == null)
                return null;

            var sql = @"INSERT INTO [ContractPayments] (
								[ContractId], 
								[Month],
								[RentPrice],
								[RenterPaymentStatus],
								[TenantPaymentStatus])
                        OUTPUT INSERTED.*
                        VALUES (
								@ContractId,
								@Month,
								@RentPrice,
								@RenterPaymentStatus,
								@TenantPaymentStatus
							);";

            var createdPaymentFromDb = _context.Connection.QuerySingle<GetPaymentForCUD>(sql,
                    new
                    {
                        payment.ContractId,
                        payment.Month,
                        RentPrice = payment.RentPrice.Price,
                        payment.RenterPaymentStatus,
                        payment.TenantPaymentStatus
                    },
                    _context.Transaction);

            var createdPaymentEntity = createdPaymentFromDb.EntityFromModel();
            return createdPaymentEntity;
        }

        public ContractPaymentEntity Update(int id, ContractPaymentEntity payment)
        {
            if (payment == null)
                return null;

            var sql = @"UPDATE 
							ContractPayments
						SET
							[ContractId] = @ContractId,
							[Month] = @Month,
							[RentPrice] = @RentPrice,
							[RenterPaymentStatus] = @RenterPaymentStatus,
							[TenantPaymentStatus] = @TenantPaymentStatus
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var updatedPaymentFromDb = _context.Connection.QuerySingle<GetPaymentForCUD>(sql,
                     new
                     {
                         Id = id,
                         payment.ContractId,
                         payment.Month,
                         RentPrice = payment.RentPrice.Price,
                         payment.RenterPaymentStatus,
                         payment.TenantPaymentStatus
                     },
                     _context.Transaction);

            var updatedPaymentEntity = updatedPaymentFromDb.EntityFromModel();
            return updatedPaymentEntity;
        }

        public ContractPaymentEntity Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							ContractPayments
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var deletedPaymentFromDb = _context.Connection.QuerySingle<GetPaymentForCUD>(sql,
                     new
                     {
                         Id = id
                     },
                     _context.Transaction);

            var deletedPaymentEntity = deletedPaymentFromDb.EntityFromModel();
            return deletedPaymentEntity;
        }
    }
}

using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
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

        public void Create(ContractPaymentEntity payment)
        {
            var sql = @"INSERT INTO [ContractPayments] (
								[ContractId], 
								[Month],
								[RentPrice],
								[RenterPaymentStatus],
								[TenantPaymentStatus]
							) VALUES (
								@ContractId,
								@Month,
								@RentPrice,
								@RenterPaymentStatus,
								@TenantPaymentStatus
							);";

            _context.Connection.Execute(sql,
                    new
                    {
                        payment.ContractId,
                        payment.Month,
                        RentPrice = payment.RentPrice.Price,
                        payment.RenterPaymentStatus,
                        payment.TenantPaymentStatus
                    },
                    _context.Transaction);
        }

        public void Update(int id, ContractPaymentEntity payment)
        {
            var sql = @"UPDATE 
							ContractPayments
						SET
							[ContractId] = @ContractId,
							[Month] = @Month,
							[RentPrice] = @RentPrice,
							[RenterPaymentStatus] = @RenterPaymentStatus,
							[TenantPaymentStatus] = @TenantPaymentStatus
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
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
        }

        public void Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							ContractPayments
						WHERE 
							Id = @Id;";

            _context.Connection.Execute(sql,
                     new
                     {
                         Id = id
                     },
                     _context.Transaction);
        }
    }
}

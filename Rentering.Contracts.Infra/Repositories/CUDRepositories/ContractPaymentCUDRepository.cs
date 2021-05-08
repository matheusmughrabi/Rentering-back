using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using System.Data;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
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
            _context.Connection.Execute("sp_ContractPayments_CUD_CreatePayment",
                    new
                    {
                        ContractId = payment.ContractId,
                        Month = payment.Month,
                        RentPrice = payment.RentPrice.Price,
                        RenterPaymentStatus = payment.RenterPaymentStatus,
                        TenantPaymentStatus = payment.TenantPaymentStatus
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Update(int id, ContractPaymentEntity payment)
        {
            _context.Connection.Execute("sp_ContractPayments_CUD_UpdatePayment",
                     new
                     {
                         Id = id,
                         ContractId = payment.ContractId,
                         Month = payment.Month,
                         RentPrice = payment.RentPrice.Price,
                         RenterPaymentStatus = payment.RenterPaymentStatus,
                         TenantPaymentStatus = payment.TenantPaymentStatus
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public void Delete(int id)
        {
            _context.Connection.Execute("sp_ContractPayments_CUD_DeletePayment",
                     new
                     {
                         Id = id
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }
    }
}

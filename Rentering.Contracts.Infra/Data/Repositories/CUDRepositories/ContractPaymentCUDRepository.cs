using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Data;

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
            _context.Connection.Execute("sp_ContractPayments_CUD_CreatePayment",
                    new
                    {
                        payment.ContractId,
                        payment.Month,
                        RentPrice = payment.RentPrice.Price,
                        payment.RenterPaymentStatus,
                        payment.TenantPaymentStatus
                    },
                    _context.Transaction,
                    commandType: CommandType.StoredProcedure
                );
        }

        public void Update(int id, ContractPaymentEntity payment)
        {
            _context.Connection.Execute("sp_ContractPayments_CUD_UpdatePayment",
                     new
                     {
                         Id = id,
                         payment.ContractId,
                         payment.Month,
                         RentPrice = payment.RentPrice.Price,
                         payment.RenterPaymentStatus,
                         payment.TenantPaymentStatus
                     },
                     _context.Transaction,
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
                     _context.Transaction,
                     commandType: CommandType.StoredProcedure
                 );
        }
    }
}

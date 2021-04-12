using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb;
using System;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class ContractPaymentCUDRepository : IContractPaymentCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractPaymentCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void CreatePaymentAnnucalCycle(ContractPaymentEntity contractPayment)
        {
            _context.Connection.Execute("spCreateContractPayment",
                     new
                     {
                         contractPayment.ContractId,
                         contractPayment.Month,
                         contractPayment.RenterPaymentStatus,
                         contractPayment.TenantPaymentStatus
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public void ExecutePayment(ContractPaymentEntity contractPayment)
        {
            _context.Connection.Execute("spExecutePayment",
                     new
                     {
                         contractPayment.ContractId,
                         contractPayment.Month,
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public void AcceptPayment(ContractPaymentEntity contractPayment)
        {
            _context.Connection.Execute("spAcceptPayment",
                     new
                     {
                         contractPayment.ContractId,
                         contractPayment.Month,
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public void RejectPayment(ContractPaymentEntity contractPayment)
        {
            _context.Connection.Execute("spRejectPayment",
                     new
                     {
                         contractPayment.ContractId,
                         contractPayment.Month,
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public ContractPaymentEntity GetContractPaymentByContractIdAndMonth(int contractId, DateTime month)
        {
            var contractPaymentFromDb = _context.Connection.Query<ContractPaymentFromDb>(
                    "spGetContractPaymentByContractIdAndMonth",
                    new
                    {
                        ContractId = contractId,
                        Month = month
                    },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            var contractPaymentEntity = new ContractPaymentEntity(contractPaymentFromDb.ContractId, contractPaymentFromDb.Month, contractPaymentFromDb.RenterPaymentStatus, contractPaymentFromDb.TenantPaymentStatus);

            return contractPaymentEntity;
        }

        public bool CheckIfContractExists(int contractId)
        {
            var contractExists = _context.Connection.Query<bool>(
                    "spCheckIfContractExists",
                    new { ContractId = contractId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return contractExists;
        }

        public bool CheckIfDateIsAlreadyRegistered(int contractId, DateTime month)
        {
            var contractExists = _context.Connection.Query<bool>(
                    "spCheckIfDateIsAlreadyRegistered",
                    new
                    {
                        ContractId = contractId,
                        Month = month
                    },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return contractExists;
        }
    }
}

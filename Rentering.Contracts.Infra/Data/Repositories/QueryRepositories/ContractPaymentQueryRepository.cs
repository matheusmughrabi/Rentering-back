using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class ContractPaymentQueryRepository : IContractPaymentQueryRepository
    {
        private readonly RenteringDataContext _context;

        public ContractPaymentQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<GetContractPaymentQueryResult> GetAll()
        {
            var sql = @"SELECT 
							*
						FROM 
							ContractPayments;";

            var paymentsFromDb = _context.Connection.Query<GetContractPaymentQueryResult>(sql);

            return paymentsFromDb;
        }

        public GetContractPaymentQueryResult GetById(int id)
        {
            var sql = @"SELECT
							*
						FROM 
							ContractPayments
						WHERE 
							[Id] = @Id;";

            var paymentFromDb = _context.Connection.Query<GetContractPaymentQueryResult>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            return paymentFromDb;
        }

        public IEnumerable<GetContractPaymentQueryResult> GetPaymentsFromContract(int contractId)
        {
            var sql = @"SELECT 
							*
						FROM 
							ContractPayments
						WHERE 
							ContractId = @ContractId;";

            var paymentsFromDb = _context.Connection.Query<GetContractPaymentQueryResult>(
                   sql,
                   new { ContractId = contractId });

            return paymentsFromDb;
        }
    }
}

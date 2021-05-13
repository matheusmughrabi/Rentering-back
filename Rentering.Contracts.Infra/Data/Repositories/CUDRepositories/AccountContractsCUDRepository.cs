using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class AccountContractsCUDRepository : IAccountContractsCUDRepository
    {
        private readonly RenteringDataContext _context;

        public AccountContractsCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void Create(AccountContractsEntity entity)
        {
            var sql = @"INSERT INTO [AccountContracts] (
								[AccountId], 
								[ContractId],
								[ParticipantRole],
								[Status]
							) VALUES (
								@AccountId,
								@ContractId,
								@ParticipantRole,
								@Status
							);";

            _context.Connection.Execute(sql,
                    new
                    {
                        entity.AccountId,
                        entity.ContractId,
                        entity.ParticipantRole,
                        entity.Status
                    },
                    _context.Transaction);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, AccountContractsEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

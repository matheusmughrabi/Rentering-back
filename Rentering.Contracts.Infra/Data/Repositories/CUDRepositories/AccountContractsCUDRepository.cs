using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
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

        public AccountContractsEntity Create(AccountContractsEntity entity)
        {
            var sql = @"INSERT INTO [AccountContracts] (
							[AccountId], 
							[ContractId],
							[ParticipantRole],
							[Status])
                        OUTPUT INSERTED.*
                        VALUES (
							@AccountId,
							@ContractId,
							@ParticipantRole,
							@Status
							);";

            var accountContractsFromDb = _context.Connection.QuerySingle<GetAccountContractsForCUD>(sql,
                    new
                    {
                        entity.AccountId,
                        entity.ContractId,
                        entity.ParticipantRole,
                        entity.Status
                    },
                    _context.Transaction);

            var accountContractsEntity = accountContractsFromDb.EntityFromModel();

            return accountContractsEntity;
        }

        public AccountContractsEntity Update(int id, AccountContractsEntity entity)
        {
            throw new NotImplementedException();
        }

        public AccountContractsEntity Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

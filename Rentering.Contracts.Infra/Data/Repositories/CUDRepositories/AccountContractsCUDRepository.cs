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

        public AccountContractsEntity GetParticipantForCUD(int accountId, int contractId)
        {
            var participantsSql = @"SELECT * FROM AccountContracts WHERE AccountId = @AccountId AND ContractId = @ContractId";

            var participantFromDb = _context.Connection.QuerySingle<GetAccountContractsForCUD>(
                 participantsSql,
                 new 
                 { 
                     AccountId = accountId,
                     ContractId = contractId 
                 });

            if (participantFromDb == null)
                return null;

            var participantEntity = participantFromDb.EntityFromModel();
            return participantEntity;
        }

        public AccountContractsEntity Create(AccountContractsEntity entity)
        {
            if (entity == null)
                return null;

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
            if (entity == null)
                return null;

            var sql = @"UPDATE 
							AccountContracts
						SET
							[ParticipantRole] = @ParticipantRole,
							[Status] = @Status
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var accountContractsFromDb = _context.Connection.QuerySingle<GetAccountContractsForCUD>(sql,
                    new
                    {
                        id,
                        entity.AccountId,
                        entity.ContractId,
                        entity.ParticipantRole,
                        entity.Status
                    },
                    _context.Transaction);

            var accountContractsEntity = accountContractsFromDb.EntityFromModel();
            return accountContractsEntity;
        }

        // TODO - Implementar Lógica de delete?
        public AccountContractsEntity Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

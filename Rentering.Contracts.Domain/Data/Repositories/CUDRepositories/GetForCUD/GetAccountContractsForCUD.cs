using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD
{
    public class GetAccountContractsForCUD : IGetForCUD<AccountContractsEntity>
    {
        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public e_ParticipantRole ParticipantRole { get; private set; }

        public AccountContractsEntity EntityFromModel()
        {
            var accountContractEntity = new AccountContractsEntity(AccountId, ContractId, ParticipantRole);
            return accountContractEntity;
        }
    }
}

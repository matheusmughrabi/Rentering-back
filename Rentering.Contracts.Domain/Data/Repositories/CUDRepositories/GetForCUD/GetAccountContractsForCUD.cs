using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD
{
    public class GetAccountContractsForCUD : IGetForCUD<AccountContractsEntity>
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ContractId { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
        public e_ParticipantStatus Status { get; set; }

        public AccountContractsEntity EntityFromModel()
        {
            var accountContractEntity = new AccountContractsEntity(AccountId, ContractId, ParticipantRole, Status, Id);
            return accountContractEntity;
        }
    }
}

using Rentering.Common.Shared.Entities;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractUserProfileEntity : Entity
    {
        public ContractUserProfileEntity(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; private set; }
        public IReadOnlyCollection<ContractEntity> Contracts { get; set; }
    }
}

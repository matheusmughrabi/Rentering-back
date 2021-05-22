using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetAccountContractsQueryResults : IQueryResult
    {
        public int AccountId { get; private set; }
        public int ContractId { get; private set; }
        public e_ParticipantRole ParticipantRole { get; private set; }
        public e_ParticipantStatus Status { get; private set; }
    }
}

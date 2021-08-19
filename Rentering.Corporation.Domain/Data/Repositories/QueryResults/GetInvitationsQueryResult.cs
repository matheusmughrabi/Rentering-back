using Rentering.Common.Shared.Data.Interfaces;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetInvitationsQueryResult : IDataResult
    {
        public int ParticipantId { get; set; }
        public int CorporationId { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
    }
}

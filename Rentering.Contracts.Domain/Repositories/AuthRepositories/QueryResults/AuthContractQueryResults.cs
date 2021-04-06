namespace Rentering.Contracts.Domain.Repositories.AuthRepositories.QueryResults
{
    public class AuthContractUserProfilesQueryResult
    {
        public AuthContractUserProfilesQueryResult()
        {
        }

        public AuthContractUserProfilesQueryResult(int id, int accountId)
        {
            Id = id;
            AccountId = accountId;
        }

        public int Id { get; set; }
        public int AccountId { get; set; }
    }

    public class AuthContracParticipantsQueryResult
    {
        public AuthContracParticipantsQueryResult()
        {
        }

        public AuthContracParticipantsQueryResult(int id, int renterId, int tenantId)
        {
            Id = id;
            RenterId = renterId;
            TenantId = tenantId;
        }

        public int Id { get; set; }
        public int RenterId { get; set; }
        public int TenantId { get; set; }
    }
}

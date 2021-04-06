namespace Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb
{
    public class UserFromDb
    {
        public UserFromDb(int accountId)
        {
            AccountId = accountId;
        }

        public int AccountId { get; set; }
    }
}

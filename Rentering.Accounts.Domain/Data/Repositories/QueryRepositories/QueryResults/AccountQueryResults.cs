using Rentering.Accounts.Domain.Enums;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetAccountQueryResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }
    }
}

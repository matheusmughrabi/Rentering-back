using Rentering.Accounts.Domain.Enums;

namespace Rentering.Accounts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetAccountQueryResultEF
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class GetAccountQueryResult_AdminUsageOnlyEF
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }
    }
}

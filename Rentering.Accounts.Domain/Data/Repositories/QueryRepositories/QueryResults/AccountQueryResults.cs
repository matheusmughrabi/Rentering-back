using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetAccountQueryResult
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class GetAccountQueryResult_AdminUsageOnly
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }
    }
}

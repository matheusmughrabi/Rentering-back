using Rentering.Accounts.Domain.Enums;

namespace Rentering.Accounts.Application.QueryResults
{
    public class GetAccountQueryResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public e_Roles Role { get; set; }
    }
}

using Rentering.Accounts.Domain.Enums;

namespace Rentering.Accounts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults
{
    public class GetAccountForCUDResult
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public e_Roles Role { get; set; }
    }
}

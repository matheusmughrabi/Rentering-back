using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.ApplicationEF.Commands.Accounts
{
    public class LoginAccountCommandEF : ICommand
    {
        public LoginAccountCommandEF(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}

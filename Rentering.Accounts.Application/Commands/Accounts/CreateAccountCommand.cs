using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands.Accounts
{
    public class CreateAccountCommand : ICommand
    {
        public CreateAccountCommand(string email, string username, string password, string confirmPassword)
        {
            Email = email;
            Username = username;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}

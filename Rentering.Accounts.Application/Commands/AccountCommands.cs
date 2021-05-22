using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Commands
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

    public class LoginAccountCommand : ICommand
    {
        public LoginAccountCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AssignAccountCommand : ICommand
    {
        public AssignAccountCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

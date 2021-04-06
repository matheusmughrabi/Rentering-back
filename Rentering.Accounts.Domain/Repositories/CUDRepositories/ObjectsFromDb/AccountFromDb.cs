using Rentering.Accounts.Domain.Enums;

namespace Rentering.Accounts.Domain.Repositories.CUDRepositories.ObjectsFromDb
{
    public class AccountFromDb
    {
        public AccountFromDb(int id, string firstName, string lastName, string email, string username, string password, e_Roles role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Username = username;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public e_Roles Role { get; set; }
    }
}

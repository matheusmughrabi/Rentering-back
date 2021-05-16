using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Entities;

namespace Rentering.Accounts.Domain.Entities
{
    public class AccountEntity : Entity
    {
        public AccountEntity(EmailValueObject email, UsernameValueObject username,
            PasswordValueObject password = null, e_Roles? role = null, int? id = null) : base(id)
        {
            Email = email;
            Username = username;
            Password = password;

            if (role != null)
                Role = (e_Roles)role;
            else
                Role = e_Roles.RegularUser;
        }

        public EmailValueObject Email { get; private set; }
        public UsernameValueObject Username { get; private set; }
        public PasswordValueObject Password { get; private set; }
        public e_Roles Role { get; private set; }
        public string Token { get; private set; }

        public void ChangeEmail(EmailValueObject email)
        {
            if (email.ToString() == Email.ToString())
            {
                AddNotification("Email", "New Email should be different than current one");
                return;
            }

            if (email.Invalid)
            {
                AddNotification("Email", "Email is invalid");
                return;
            }

            Email = email;
        }

        public void ChangeUsername(UsernameValueObject username)
        {
            if (username.ToString() == Username.ToString())
            {
                AddNotification("Username", "New Username should be different than current one");
                return;
            }

            if (username.Invalid)
            {
                AddNotification("Username", "Username is invalid");
                return;
            }

            Username = username;
        }

        public void AssignAdminRole()
        {
            if (Role == e_Roles.Admin)
            {
                AddNotification("Role", "This user is already admin");
                return;
            }

            Role = e_Roles.Admin;
        }
    }
}
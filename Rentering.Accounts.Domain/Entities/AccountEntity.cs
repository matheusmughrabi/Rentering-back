using Rentering.Accounts.Domain.SafeEnums;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Rentering.Accounts.Domain.Entities
{
    public class AccountEntity : Entity
    {
        protected AccountEntity()
        {
        }

        public AccountEntity(
            PersonNameValueObject name,
            EmailValueObject email, 
            UsernameValueObject username,
            PasswordValueObject password = null,
            RoleType role = null, 
            int? id = null) : base(id)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;

            if (role != null)
                Role = role;
            else
                Role = RoleType.RegularUser;
        }

        [Required]
        public PersonNameValueObject Name { get; private set; }
        [Required]
        public EmailValueObject Email { get; private set; }
        [Required]
        public UsernameValueObject Username { get; private set; }
        [Required]
        public PasswordValueObject Password { get; private set; }
        [Required]
        public RoleType Role { get; private set; }
        public string Token { get; private set; }

        public void ChangeEmail(EmailValueObject email)
        {
            if (email.ToString() == Email.ToString())
            {
                AddNotification("Email", "O novo email precisa ser diferente do atual.");
                return;
            }

            if (email.Invalid)
            {
                AddNotification("Email", "Email inválido");
                return;
            }

            Email = email;
        }

        public void ChangeUsername(UsernameValueObject username)
        {
            if (username.ToString() == Username.ToString())
            {
                AddNotification("Usuário", "Novo nome de usuário precisa ser diferente do atual.");
                return;
            }

            if (username.Invalid)
            {
                AddNotification("Usuário", "Nome de usuário inválido.");
                return;
            }

            Username = username;
        }
    }
}
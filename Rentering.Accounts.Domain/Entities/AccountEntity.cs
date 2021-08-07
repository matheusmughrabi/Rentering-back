using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Entities;
using System.Collections.Generic;
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
            string password = null,
            e_Role? role = null, 
            int? id = null) : base(id)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            License = e_License.Free;

            if (role != null)
                Role = (e_Role)role;
            else
                Role = e_Role.RegularUser;
        }

        [Required]
        public PersonNameValueObject Name { get; private set; }
        [Required]
        public EmailValueObject Email { get; private set; }
        [Required]
        public UsernameValueObject Username { get; private set; }
        public string Password { get; set; }
        public e_Role Role { get; private set; }
        public string Token { get; private set; }
        public e_License License { get; set; }

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
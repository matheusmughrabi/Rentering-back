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
            ERole? role = null, 
            int? id = null) : base(id)
        {
            Name = name;
            Email = email;
            Username = username;
            Password = password;
            LicenseCode = 1;

            if (role != null)
                Role = (ERole)role;
            else
                Role = ERole.RegularUser;
        }

        [Required]
        public PersonNameValueObject Name { get; private set; }
        [Required]
        public EmailValueObject Email { get; private set; }
        [Required]
        public UsernameValueObject Username { get; private set; }
        public string Password { get; set; }
        public ERole Role { get; private set; }
        public string Token { get; private set; }
        public int LicenseCode { get; set; }

        public void ChangeLicense(int licenseCode)
        {
            if (LicenseCode == licenseCode)
            {
                AddNotification("Licen�a", "A nova licen�a precisa ser diferente da atual.");
                return;
            }

            LicenseCode = licenseCode;
        }

        public void ChangeEmail(EmailValueObject email)
        {
            if (email.ToString() == Email.ToString())
            {
                AddNotification("Email", "O novo email precisa ser diferente do atual.");
                return;
            }

            if (email.Invalid)
            {
                AddNotification("Email", "Email inv�lido");
                return;
            }

            Email = email;
        }

        public void ChangeUsername(UsernameValueObject username)
        {
            if (username.ToString() == Username.ToString())
            {
                AddNotification("Usu�rio", "Novo nome de usu�rio precisa ser diferente do atual.");
                return;
            }

            if (username.Invalid)
            {
                AddNotification("Usu�rio", "Nome de usu�rio inv�lido.");
                return;
            }

            Username = username;
        }
    }
}
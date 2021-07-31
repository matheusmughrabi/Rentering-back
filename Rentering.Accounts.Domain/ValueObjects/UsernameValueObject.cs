using FluentValidator.Validation;
using Rentering.Common.Shared.ValueObjects;

namespace Rentering.Accounts.Domain.ValueObjects
{
    public class UsernameValueObject : BaseValueObject
    {
        private UsernameValueObject()
        {
        }

        public UsernameValueObject(string username)
        {
            Username = username;

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Username, 3, "Usuário", "O nome de usuário precisa ter ao menos 3 letras")
                .HasMaxLen(Username, 40, "Usuário", "O nome de usuário precisa ter menos do que 40 letras")
            );
        }

        public string Username { get; private set; }

        public override string ToString()
        {
            return Username;
        }
    }
}

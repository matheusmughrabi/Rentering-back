using FluentValidator;
using Rentering.Accounts.ApplicationEF.Commands.Accounts;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Accounts.InfraEF;
using Rentering.Common.Shared.Commands;
using System.Linq;

namespace Rentering.Accounts.ApplicationEF.Handlers
{
    public class AccountHandlers : Notifiable,
        IHandler<CreateAccountCommandEF>,
        IHandler<AssignAdminRoleAccountCommandEF>
    {
        private readonly AccountsDbContext _accountsDbContext;

        public AccountHandlers(AccountsDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public ICommandResult Handle(CreateAccountCommandEF command)
        {
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(email, username, password);

            var emailExists = _accountsDbContext.Account.Any(c => c.Email.Email == command.Email);
            var usernameExists = _accountsDbContext.Account.Any(c => c.Username.Username == command.Username);

            if (emailExists)
                AddNotification("Email", "This Email is already registered");

            if (usernameExists)
                AddNotification("Username", "This Username is already registered");

            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountsDbContext.Account.Add(accountEntity);
            _accountsDbContext.SaveChanges();
            _accountsDbContext.Dispose();

            var createdUser = new CommandResult(true, "User created successfuly", new
            {
                command.Email,
                command.Username,
                accountEntity.Role
            });

            return createdUser;
        }

        public ICommandResult Handle(AssignAdminRoleAccountCommandEF command)
        {
            var accountEntity = _accountsDbContext.Account
                .Where(c => c.Id == command.Id)
                .FirstOrDefault();

            if (accountEntity == null)
                return new CommandResult(false, "Account not found", new { });

            accountEntity.AssignAdminRole();

            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountsDbContext.SaveChanges();
            _accountsDbContext.Dispose();

            var adminRoleAssignedUser = new CommandResult(true, "Admin role assigned successfuly", new
            {
                accountEntity.Email.Email,
                accountEntity.Username.Username,
                accountEntity.Role
            });

            return adminRoleAssignedUser;
        }
    }
}

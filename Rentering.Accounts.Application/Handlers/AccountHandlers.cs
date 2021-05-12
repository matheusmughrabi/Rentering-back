using FluentValidator;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Handlers
{
    public class AccountHandlers : Notifiable,
        IHandler<CreateAccountCommand>,
        IHandler<AssignAccountCommand>
    {
        private readonly IAccountUnitOfWork _accountUnitOfWork;

        public AccountHandlers(IAccountUnitOfWork accountUnitOfWork)
        {
            _accountUnitOfWork = accountUnitOfWork;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(email, username, password);

            if (_accountUnitOfWork.AccountQuery.CheckIfEmailExists(command.Email))
                AddNotification("Email", "This Email is already registered");

            if (_accountUnitOfWork.AccountQuery.CheckIfUsernameExists(command.Username))
                AddNotification("Username", "This Username is already registered");

            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountUnitOfWork.AccountCUD.Create(accountEntity);

            var createdUser = new CommandResult(true, "User created successfuly", new
            {
                command.Email,
                command.Username,
                accountEntity.Role
            });

            return createdUser;
        }

        public ICommandResult Handle(AssignAccountCommand command)
        {
            var id = command.Id;
            var accountEntity = _accountUnitOfWork.AccountCUD.GetAccountForCUD(command.Id);

            accountEntity.AssignAdminRole();

            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountUnitOfWork.AccountCUD.Update(id, accountEntity);

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

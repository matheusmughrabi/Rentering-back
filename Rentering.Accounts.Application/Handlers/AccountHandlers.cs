using FluentValidator;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Handlers
{
    public class AccountHandlers : Notifiable,
        IHandler<RegisterCommand>
    {
        private readonly IAccountUnitOfWork _accountsUnitOfWork;

        public AccountHandlers(IAccountUnitOfWork accountsUnitOfWork)
        {
            _accountsUnitOfWork = accountsUnitOfWork;
        }

        public ICommandResult Handle(RegisterCommand command)
        {
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(email, username, password);

            if (_accountsUnitOfWork.AccountCUDRepository.EmailExists(command.Email))
                AddNotification("Email", "This Email is already registered");

            if (_accountsUnitOfWork.AccountCUDRepository.UsernameExists(command.Username))
                AddNotification("Username", "This Username is already registered");

            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountsUnitOfWork.AccountCUDRepository.Add(accountEntity);
            _accountsUnitOfWork.Save();

            var createdUser = new CommandResult(true, "User created successfuly", new
            {
                command.Email,
                command.Username,
                accountEntity.Role
            });

            return createdUser;
        }
    }
}

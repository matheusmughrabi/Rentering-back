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
            var name = new PersonNameValueObject(command.FirstName, command.LastName);
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(name, email, username, password);

            if (_accountsUnitOfWork.AccountCUDRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está registrado");

            if (_accountsUnitOfWork.AccountCUDRepository.UsernameExists(command.Username))
                AddNotification("Username", "Este nome de usuário já está registrado");

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", new { Notifications });

            _accountsUnitOfWork.AccountCUDRepository.Add(accountEntity);
            _accountsUnitOfWork.Save();

            var createdUser = new CommandResult(true, "Usuário criado com sucesso!", new
            {
                command.Email,
                command.Username,
                accountEntity.Role
            });

            return createdUser;
        }
    }
}

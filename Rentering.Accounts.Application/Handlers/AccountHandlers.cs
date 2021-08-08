using FluentValidator;
using Microsoft.AspNetCore.Identity;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Enums;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Handlers
{
    public class AccountHandlers : Notifiable,
        IHandler<RegisterCommand>,
        IHandler<PayLicenseCommand>
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

            var accountEntity = new AccountEntity(name, email, username, command.Password);

            var passwordHasher = new PasswordHasher<AccountEntity>();
            string hashedPassword = passwordHasher.HashPassword(accountEntity, command.Password);
            accountEntity.Password = hashedPassword;

            if (_accountsUnitOfWork.AccountCUDRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está registrado");

            if (_accountsUnitOfWork.AccountCUDRepository.UsernameExists(command.Username))
                AddNotification("Username", "Este nome de usuário já está registrado");

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Corrija os erros abaixo.", Notifications.ConvertCommandNotifications(), null);

            _accountsUnitOfWork.AccountCUDRepository.Add(accountEntity);
            _accountsUnitOfWork.Save();

            var createdUser = new CommandResult(true, "Usuário criado com sucesso!", null, new
            {
                command.Email,
                command.Username,
                accountEntity.Role
            });

            return createdUser;
        }

        public ICommandResult Handle(PayLicenseCommand command)
        {
            var accountEntity = _accountsUnitOfWork.AccountCUDRepository.GetAccountForCUD(command.CurrentUserId);
            if (accountEntity == null)
            {
                AddNotification("Conta", "Conta não foi encontrada");
                return new CommandResult(false, "Problema ao trocar licença.", Notifications.ConvertCommandNotifications(), null);
            }

            accountEntity.ChangeLicense((e_License)command.License);

            AddNotifications(accountEntity);

            if (Invalid)
                return new CommandResult(false, "Problema ao trocar licença.", Notifications.ConvertCommandNotifications(), null);

            _accountsUnitOfWork.Save();

            var result = new CommandResult(true, "Licença adquirida com sucesso!", null, null);

            return result;
        }
    }
}

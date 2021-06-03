﻿using FluentValidator;
using Rentering.Accounts.Application.Commands.Accounts;
using Rentering.Accounts.Domain.Data;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Commands;

namespace Rentering.Accounts.Application.Handlers
{
    public class AccountHandlers : Notifiable,
        IHandler<CreateAccountCommandEF>,
        IHandler<AssignAdminRoleAccountCommandEF>
    {
        private readonly IAccountUnitOfWorkEF _accountsUnitOfWorkEF;

        public AccountHandlers(IAccountUnitOfWorkEF accountsUnitOfWorkEF)
        {
            _accountsUnitOfWorkEF = accountsUnitOfWorkEF;
        }

        public ICommandResult Handle(CreateAccountCommandEF command)
        {
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(email, username, password);

            if (_accountsUnitOfWorkEF.AccountCUDRepositoryEF.EmailExists(command.Email))
                AddNotification("Email", "This Email is already registered");

            if (_accountsUnitOfWorkEF.AccountCUDRepositoryEF.UsernameExists(command.Username))
                AddNotification("Username", "This Username is already registered");

            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountsUnitOfWorkEF.AccountCUDRepositoryEF.Add(accountEntity);
            _accountsUnitOfWorkEF.Save();

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
            var accountEntity = _accountsUnitOfWorkEF.AccountCUDRepositoryEF.GetAccountForCUD(command.Id);

            if (accountEntity == null)
                return new CommandResult(false, "Account not found", new { });

            accountEntity.AssignAdminRole();

            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountsUnitOfWorkEF.Save();

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

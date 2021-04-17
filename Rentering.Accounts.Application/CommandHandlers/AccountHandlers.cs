using FluentValidator;
using Rentering.Accounts.Application.Commands;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Shared.Commands;
using System.Linq;

namespace Rentering.Accounts.Application.CommandHandlers
{
    public class AccountHandlers : Notifiable,
        ICommandHandler<CreateAccountCommand>,
        ICommandHandler<AssignAccountCommand>
    {
        private readonly IAccountCUDRepository _accountRepository;

        public AccountHandlers(IAccountCUDRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(email, username, password);

            if (_accountRepository.CheckIfEmailExists(command.Email))
                AddNotification("Email", "This Email is already registered");

            if (_accountRepository.CheckIfUsernameExists(command.Username))
                AddNotification("Username", "This Username is already registered");

            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountRepository.CreateAccount(accountEntity);

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
            var userEntityFromDb = _accountRepository.GetAccountById(command.Id);

            userEntityFromDb.AssignAdminRole();

            AddNotifications(userEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountRepository.UpdateAccount(id, userEntityFromDb);

            var adminRoleAssignedUser = new CommandResult(true, "Admin role assigned successfuly", new
            {
                userEntityFromDb.Email.Email,
                userEntityFromDb.Username.Username,
                userEntityFromDb.Role
            });

            return adminRoleAssignedUser;
        }
    }
}

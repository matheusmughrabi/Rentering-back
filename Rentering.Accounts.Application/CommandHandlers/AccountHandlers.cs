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
        ICommandHandler<LoginAccountCommand>,
        ICommandHandler<AssignAccountCommand>
    {
        private readonly IAccountCUDRepository _accountRepository;

        public AccountHandlers(IAccountCUDRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ICommandResult Handle(CreateAccountCommand command)
        {
            var name = new NameValueObject(command.FirstName, command.LastName);
            var email = new EmailValueObject(command.Email);
            var username = new UsernameValueObject(command.Username);
            var password = new PasswordValueObject(command.Password, command.ConfirmPassword);
            var accountEntity = new AccountEntity(name, email, username, password);

            //if (_userRepository.CheckIfEmailExists(command.Email))
            //    AddNotification("Email", "This Email is already registered");

            //if (_userRepository.CheckIfUsernameExists(command.Username))
            //    AddNotification("Username", "This Username is already registered");

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(username.Notifications);
            AddNotifications(password.Notifications);
            AddNotifications(accountEntity.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountRepository.CreateAccount(accountEntity);

            var createdUser = new CommandResult(true, "User created successfuly", new
            {
                command.FirstName,
                command.LastName,
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

            //if (_userRepository.CheckIfUsernameExists(userEntityFromDb.Username.Username))
            //    AddNotification("Username", "This Username is already registered");

            AddNotifications(userEntityFromDb.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            _accountRepository.UpdateAccount(id, userEntityFromDb);

            var adminRoleAssignedUser = new CommandResult(true, "Admin role assigned successfuly", new
            {
                userEntityFromDb.Name.FirstName,
                userEntityFromDb.Name.LastName,
                userEntityFromDb.Email.Email,
                userEntityFromDb.Username.Username,
                userEntityFromDb.Role
            });

            return adminRoleAssignedUser;
        }

        public ICommandResult Handle(LoginAccountCommand command)
        {
            var accountEntitiesFromDb = _accountRepository.GetAllAccounts();

            var accountFiltered = accountEntitiesFromDb
                .Where(c => c.Username.Username == command.Username && c.Password.Password == command.Password)
                .FirstOrDefault();

            //if (_userRepository.CheckIfUsernameExists(userEntityFromDb.Username.Username))
            //    AddNotification("Username", "This Username is already registered");

            AddNotifications(accountFiltered.Notifications);

            if (Invalid)
                return new CommandResult(false, "Fix erros below", new { Notifications });

            var accountCommandResult = new CommandResult(true, "User retrieved", new
            {
                accountFiltered.Name.FirstName,
                accountFiltered.Name.LastName,
                accountFiltered.Email.Email,
                accountFiltered.Username.Username,
                accountFiltered.Role
            });

            return accountCommandResult;
        }
    }
}

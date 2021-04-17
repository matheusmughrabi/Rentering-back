using Dapper;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Repositories.CUDRepositories.ObjectsFromDb;
using Rentering.Accounts.Domain.ValueObjects;
using Rentering.Common.Infra;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Accounts.Infra.Repositories.CUDRepositories
{
    public class AccountCUDRepository : IAccountCUDRepository
    {
        private readonly RenteringDataContext _context;

        public AccountCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfEmailExists(string email)
        {
            var emailExists = _context.Connection.Query<bool>(
                     "sp_Accounts_Util_CheckIfEmailExists",
                     new { Email = email },
                     commandType: CommandType.StoredProcedure
                 ).FirstOrDefault();

            return emailExists;
        }

        public bool CheckIfUsernameExists(string username)
        {
            var documentExists = _context.Connection.Query<bool>(
                    "sp_Accounts_Util_CheckIfUsernameExists",
                    new { Username = username },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return documentExists;
        }

        public void CreateAccount(AccountEntity accountEntity)
        {
            _context.Connection.Execute("sp_Accounts_CUD_CreateAccount",
                     new
                     {
                         accountEntity.Email.Email,
                         accountEntity.Username.Username,
                         accountEntity.Password.Password,
                         accountEntity.Role
                     },
                     commandType: CommandType.StoredProcedure
                 );
        }

        public AccountEntity GetAccountById(int id)
        {
            var accountFromDb = _context.Connection.Query<AccountFromDb>(
                    "sp_Accounts_CUD_GetAccountById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            var email = new EmailValueObject(accountFromDb.Email);
            var username = new UsernameValueObject(accountFromDb.Username);
            var password = new PasswordValueObject(accountFromDb.Password);

            var accountEntity = new AccountEntity(email, username, password, accountFromDb.Role);

            return accountEntity;
        }

        public void UpdateAccount(int id, AccountEntity accountEntity)
        {
            _context.Connection.Execute("sp_Accounts_CUD_UpdateAccount",
                    new
                    {
                        Id = id,
                        accountEntity.Email.Email,
                        accountEntity.Username.Username,
                        accountEntity.Password.Password,
                        accountEntity.Role
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteAccount(int id)
        {
            _context.Connection.Execute("sp_Accounts_CUD_DeleteAccount",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
        }

        public IEnumerable<AccountEntity> GetAllAccounts()
        {
            var accountsFromDb = _context.Connection.Query<AccountFromDb>(
                     "sp_Accounts_CUD_GetAllAccounts",
                     commandType: CommandType.StoredProcedure
                 );

            var accountsEntities = new List<AccountEntity>();
            foreach (var accountFromDb in accountsFromDb)
            {
                var email = new EmailValueObject(accountFromDb.Email);
                var username = new UsernameValueObject(accountFromDb.Username);
                var password = new PasswordValueObject(accountFromDb.Password);

                var accountEntity = new AccountEntity(email, username, password, accountFromDb.Role, accountFromDb.Id);
                accountsEntities.Add(accountEntity);
            }

            return accountsEntities;
        }
    }
}

using Dapper;
using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Accounts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Accounts.Domain.Entities;
using Rentering.Common.Infra;
using System.Linq;

namespace Rentering.Accounts.Infra.Data.Repositories.CUDRepositories
{
    public class AccountCUDRepository : IAccountCUDRepository
    {
        private readonly RenteringDataContext _context;

        public AccountCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public AccountEntity GetAccountForCUD(int id)
        {
            var sql = @"SELECT Id, Email, Username, Role FROM Accounts WHERE Id = @Id;";

            var accountFromDb = _context.Connection.Query<GetAccountForCUD>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            if (accountFromDb == null)
                return null;

            var accountEntity = accountFromDb.EntityFromModel();

            return accountEntity;
        }

        public AccountEntity GetAccountForLoginCUD(string username)
        {
            var sql = @"SELECT Id, Email, Username, Password, Role FROM Accounts WHERE Username = @Username;";

            var accountFromDb = _context.Connection.Query<GetAccountForLoginCUD>(
                    sql,
                    new { Username = username }).FirstOrDefault();

            if (accountFromDb == null)
                return null;

            var accountEntity = accountFromDb.EntityFromModel();

            return accountEntity;
        }

        public AccountEntity Create(AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var sql = @" INSERT INTO [Accounts] (
                                [Email], 
                                [Username], 
                                [Password],
		                        [Role]) 
                        OUTPUT INSERTED.*
                        VALUES (
                                @Email,
                                @Username,
                                @Password,
		                        @Role
                            );";

            var accountFromDb = _context.Connection.QuerySingle<GetAccountForCUD>(sql,
                     new
                     {
                         accountEntity.Email.Email,
                         accountEntity.Username.Username,
                         accountEntity.Password.Password,
                         accountEntity.Role
                     });

            var createdAccountEntity = accountFromDb.EntityFromModel();
            return createdAccountEntity;
        }

        public AccountEntity Update(int id, AccountEntity accountEntity)
        {
            if (accountEntity == null)
                return null;

            var sql = @"UPDATE 
		                        Accounts
	                        SET 
		                        Email = @Email,
		                        Username = @Username,
		                        Role = @Role
                            OUTPUT INSERTED.*
	                        WHERE 
		                        Id = @Id;";

            var accountFromDb = _context.Connection.QuerySingle<GetAccountForCUD>(sql,
                    new
                    {
                        Id = id,
                        accountEntity.Email.Email,
                        accountEntity.Username.Username,
                        accountEntity.Role
                    });

            var updatedAccountEntity = accountFromDb.EntityFromModel();
            return updatedAccountEntity;
        }

        public AccountEntity Delete(int id)
        {
            var sql = @"DELETE FROM 
		                        Accounts
                            OUTPUT INSERTED.*
	                        WHERE 
		                        Id = @Id;";

            var accountFromDb = _context.Connection.QuerySingle<GetAccountForCUD>(sql,
                    new
                    { 
                        Id = id 
                    });

            var deletedAccountEntity = accountFromDb.EntityFromModel();
            return deletedAccountEntity;
        }
    }
}

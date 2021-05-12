﻿using Dapper;
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

            var accountEntity = accountFromDb.EntityFromModel();

            return accountEntity;
        }

        public AccountEntity GetAccountForLoginCUD(string username)
        {
            var sql = @"SELECT Id, Email, Username, Password, Role FROM Accounts WHERE Username = @Username;";

            var accountFromDb = _context.Connection.Query<GetAccountForLoginCUD>(
                    sql,
                    new { Username = username }).FirstOrDefault();

            var accountEntity = accountFromDb.EntityFromModel();

            return accountEntity;
        }

        public void Create(AccountEntity accountEntity)
        {
            var sql = @" INSERT INTO [Accounts] (
                                [Email], 
                                [Username], 
                                [Password],
		                        [Role]
                            ) VALUES (
                                @Email,
                                @Username,
                                @Password,
		                        @Role
                            );";

            _context.Connection.Execute(sql,
                     new
                     {
                         accountEntity.Email.Email,
                         accountEntity.Username.Username,
                         accountEntity.Password.Password,
                         accountEntity.Role
                     });
        }

        public void Update(int id, AccountEntity accountEntity)
        {
            var sql = @"UPDATE 
		                        Accounts
	                        SET 
		                        Email = @Email,
		                        Username = @Username,
		                        Role = @Role
	                        WHERE 
		                        Id = @Id;";

            _context.Connection.Execute(sql,
                    new
                    {
                        Id = id,
                        accountEntity.Email.Email,
                        accountEntity.Username.Username,
                        accountEntity.Role
                    });
        }

        public void Delete(int id)
        {
            var sql = @"DELETE FROM 
		                        Accounts
	                        WHERE 
		                        Id = @Id;";

            _context.Connection.Execute(sql,
                    new
                    { 
                        Id = id 
                    });
        }
    }
}

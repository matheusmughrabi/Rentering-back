﻿using Dapper;
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

        //public bool CheckIfEmailExists(string email)
        //{
        //    var emailExists = _context.Connection.Query<bool>(
        //             "spCheckIfEmailExists",
        //             new { Email = email },
        //             commandType: CommandType.StoredProcedure
        //         ).FirstOrDefault();

        //    return emailExists;
        //}

        //public bool CheckIfUsernameExists(string username)
        //{
        //    var documentExists = _context.Connection.Query<bool>(
        //            "spCheckIfUsernameExists",
        //            new { Username = username },
        //            commandType: CommandType.StoredProcedure
        //        ).FirstOrDefault();

        //    return documentExists;
        //}

        public void CreateAccount(AccountEntity accountEntity)
        {
            _context.Connection.Execute("spCreateAccount",
                     new
                     {
                         accountEntity.Name.FirstName,
                         accountEntity.Name.LastName,
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
                    "spGetAccountById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            var name = new NameValueObject(accountFromDb.FirstName, accountFromDb.LastName);
            var email = new EmailValueObject(accountFromDb.Email);
            var username = new UsernameValueObject(accountFromDb.Username);
            var password = new PasswordValueObject(accountFromDb.Password);

            var accountEntity = new AccountEntity(name, email, username, password, accountFromDb.Role);

            return accountEntity;
        }

        public void UpdateAccount(int id, AccountEntity accountEntity)
        {
            _context.Connection.Execute("spUpdateAccount",
                    new
                    {
                        Id = id,
                        accountEntity.Name.FirstName,
                        accountEntity.Name.LastName,
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
            _context.Connection.Execute("spDeleteAccount",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
        }

        public IEnumerable<AccountEntity> GetAllAccounts()
        {
            var accountsFromDb = _context.Connection.Query<AccountFromDb>(
                     "spGetAllAccounts",
                     commandType: CommandType.StoredProcedure
                 );

            var accountsEntities = new List<AccountEntity>();
            foreach (var accountFromDb in accountsFromDb)
            {
                var name = new NameValueObject(accountFromDb.FirstName, accountFromDb.LastName);
                var email = new EmailValueObject(accountFromDb.Email);
                var username = new UsernameValueObject(accountFromDb.Username);
                var password = new PasswordValueObject(accountFromDb.Password);

                var accountEntity = new AccountEntity(name, email, username, password, accountFromDb.Role, accountFromDb.Id);
                accountsEntities.Add(accountEntity);
            }

            return accountsEntities;
        }
    }
}
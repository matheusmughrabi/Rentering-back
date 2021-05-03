using Dapper;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.Repositories.CUDRepositories;
using Rentering.Common.Infra;
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
    }
}

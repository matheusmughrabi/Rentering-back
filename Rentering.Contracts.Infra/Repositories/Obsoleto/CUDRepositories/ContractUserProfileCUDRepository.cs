using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class ContractUserProfileCUDRepository : IContractUserProfileCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractUserProfileCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfAccountExists(int accountId)
        {
            var accountExists = _context.Connection.Query<bool>(
                    "spCheckIfAccountExists",
                    new { Id = accountId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            return accountExists;
        }

        public void CreateContractUserProfile(ContractUserProfileEntity contractPofileUser)
        {
            _context.Connection.Execute("spCreateContractProfileUser",
                    new
                    {
                        contractPofileUser.AccountId
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteContractUserProfile(int id)
        {
            _context.Connection.Execute("spDeleteContractUserProfile",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
        }

        public ContractUserProfileEntity GetContractUserProfileById(int id)
        {
            var contractUserProfileFromDb = _context.Connection.Query<UserFromDb>(
                    "spGetContractUserProfileById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            var contractUserProfile = new ContractUserProfileEntity(contractUserProfileFromDb.AccountId);

            return contractUserProfile;
        }
    }
}

using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb;
using Rentering.Contracts.Domain.ValueObjects;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Repositories.CUDRepositories
{
    public class ContractCUDRepository : IContractCUDRepository
    {
        private readonly RenteringDataContext _context;

        public ContractCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public void CreateContract(ContractEntity contract)
        {
            _context.Connection.Execute("spCreateContract",
                    new
                    {
                        contract.ContractName,
                        RentPrice = contract.RentPrice.Price,
                        contract.RenterId,
                        TenantId = contract.TentantId
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void UpdateContractRentPrice(int id, ContractEntity contract)
        {
            _context.Connection.Execute("spUpdateContractRentPrice",
                    new
                    {
                        Id = id,
                        RentPrice = contract.RentPrice.Price
                    },
                    commandType: CommandType.StoredProcedure
                );
        }

        public void DeleteContract(int id)
        {
            _context.Connection.Execute("spDeleteContract",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                );
        }

        ContractEntity IContractCUDRepository.GetContractById(int id)
        {
            var contractFromDb = _context.Connection.Query<ContractFromDb>(
                    "spGetContractById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

            var rentPrice = new PriceValueObject(contractFromDb.RentPrice);
            var contractEntity = new ContractEntity(contractFromDb.ContractName, rentPrice, contractFromDb.RenterId, contractFromDb.TenantId);

            return contractEntity;
        }

        public bool CheckIfContractNameExists(string contractName)
        {
            var contractNameExists = _context.Connection.Query<bool>(
                     "spCheckIfContractNameExists",
                     new { ContractName = contractName },
                     commandType: CommandType.StoredProcedure
                 ).FirstOrDefault();

            return contractNameExists;
        }

        public bool CheckIfContractUserProfileExists(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Contracts.CUDRepositories
{
    public class ContractCUDRepository : IContractCUDRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public ContractCUDRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public ContractEntity GetContractForCUD(int contractId)
        {
            var contractEntity = _renteringDbContext.Contract
                .Where(c => c.Id == contractId)
                .Include(c => c.Participants)
                .Include(c => c.Payments)
                .FirstOrDefault();

            return contractEntity;
        }

        public bool ContractNameExists(string contractName)
        {
            var contractNameExists = _renteringDbContext.Contract
                .AsNoTracking()
                .Any(c => c.ContractName == contractName);

            return contractNameExists;
        }

        public ContractEntity Add(ContractEntity contractEntity)
        {
            if (contractEntity == null)
                return null;

            var addedContractEntity = _renteringDbContext.Contract.Add(contractEntity).Entity;
            return addedContractEntity;
        }

        public ContractEntity Delete(ContractEntity contractEntity)
        {
            if (contractEntity == null)
                return null;

            var deletedContractEntity = _renteringDbContext.Contract.Remove(contractEntity).Entity;
            return deletedContractEntity;
        }

        public ContractEntity Delete(int id)
        {
            var contractEntity = _renteringDbContext.Contract
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (contractEntity == null)
                return null;

            var deletedContract = _renteringDbContext.Remove(contractEntity).Entity;
            return deletedContract;
        }
    }
}

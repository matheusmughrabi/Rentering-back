using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Contracts.CUDRepositories
{
    public class EstateContractCUDRepository : IEstateContractCUDRepository
    {
        private readonly RenteringDbContext _renteringDbContext;

        public EstateContractCUDRepository(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public ContractEntity GetEstateContractForCUD(int estateContractId)
        {
            var estateContractEntity = _renteringDbContext.Contract
                .Where(c => c.Id == estateContractId)
                .Include(c => c.Participants)
                .Include(c => c.Payments)
                .FirstOrDefault();

            return estateContractEntity;
        }

        public bool ContractNameExists(string contractName)
        {
            var contractNameExists = _renteringDbContext.Contract
                .AsNoTracking()
                .Any(c => c.ContractName == contractName);

            return contractNameExists;
        }

        public ContractEntity Add(ContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var addedEstateContractEntity = _renteringDbContext.Contract.Add(estateContractEntity).Entity;
            return addedEstateContractEntity;
        }

        public ContractEntity Delete(ContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var deletedEstateContractEntity = _renteringDbContext.Contract.Remove(estateContractEntity).Entity;
            return deletedEstateContractEntity;
        }

        public ContractEntity Delete(int id)
        {
            var estateContractEntity = _renteringDbContext.Contract
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (estateContractEntity == null)
                return null;

            var deletedEstateContract = _renteringDbContext.Remove(estateContractEntity).Entity;
            return deletedEstateContract;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.DataEF.Repositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.InfraEF.Repositories
{
    public class EstateContractCUDRepositoryEF : IEstateContractCUDRepositoryEF
    {
        private readonly ContractsDbContext _contractsDbContext;

        public EstateContractCUDRepositoryEF(ContractsDbContext contractsDbContext)
        {
            _contractsDbContext = contractsDbContext;
        }

        public EstateContractEntity GetEstateContractForCUD(int estateContractId)
        {
            var estateContractEntity = _contractsDbContext.Contract
                .Where(c => c.Id == estateContractId)
                .Include(c => c.Participants)
                .Include(c => c.Renters)
                .Include(c => c.Tenants)
                .Include(c => c.Guarantors)
                .Include(c => c.Payments)
                .FirstOrDefault();

            return estateContractEntity;
        }

        public bool ContractNameExists(string contractName)
        {
            var contractNameExists = _contractsDbContext.Contract
                .AsNoTracking()
                .Any(c => c.ContractName == contractName);

            return contractNameExists;
        }

        public EstateContractEntity Add(EstateContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var addedEstateContractEntity = _contractsDbContext.Contract.Add(estateContractEntity).Entity;
            return addedEstateContractEntity;
        }

        public EstateContractEntity Delete(EstateContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var deletedEstateContractEntity = _contractsDbContext.Contract.Remove(estateContractEntity).Entity;
            return deletedEstateContractEntity;
        }

        public EstateContractEntity Delete(int id)
        {
            var estateContractEntity = _contractsDbContext.Contract
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (estateContractEntity == null)
                return null;

            var deletedEstateContract = _contractsDbContext.Remove(estateContractEntity).Entity;
            return deletedEstateContract;
        }
    }
}

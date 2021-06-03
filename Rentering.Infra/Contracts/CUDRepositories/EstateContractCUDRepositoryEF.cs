using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.DataEF.CUDRepositories;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Infra.Contracts.CUDRepositories
{
    public class EstateContractCUDRepositoryEF : IEstateContractCUDRepositoryEF
    {
        private readonly RenteringDbContext _renteringDbContext;

        public EstateContractCUDRepositoryEF(RenteringDbContext renteringDbContext)
        {
            _renteringDbContext = renteringDbContext;
        }

        public EstateContractEntity GetEstateContractForCUD(int estateContractId)
        {
            var estateContractEntity = _renteringDbContext.Contract
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
            var contractNameExists = _renteringDbContext.Contract
                .AsNoTracking()
                .Any(c => c.ContractName == contractName);

            return contractNameExists;
        }

        public EstateContractEntity Add(EstateContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var addedEstateContractEntity = _renteringDbContext.Contract.Add(estateContractEntity).Entity;
            return addedEstateContractEntity;
        }

        public EstateContractEntity Delete(EstateContractEntity estateContractEntity)
        {
            if (estateContractEntity == null)
                return null;

            var deletedEstateContractEntity = _renteringDbContext.Contract.Remove(estateContractEntity).Entity;
            return deletedEstateContractEntity;
        }

        public EstateContractEntity Delete(int id)
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

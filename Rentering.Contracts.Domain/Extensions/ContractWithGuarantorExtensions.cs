using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class ContractWithGuarantorExtensions
    {
        public static ContractWithGuarantorEntity EntityFromModel(this GetContractWithGuarantorQueryResult contractQueryResult)
        {
            if (contractQueryResult == null)
                return null;

            var id = contractQueryResult.Id;
            var contractName = contractQueryResult.ContractName;
            var renterId = contractQueryResult.RenterId;
            var tenantId = contractQueryResult.TenantId;
            var guarantorId = contractQueryResult.GuarantorId;
            var address = new AddressValueObject(contractQueryResult.Street, contractQueryResult.Neighborhood, contractQueryResult.City,
                contractQueryResult.CEP, contractQueryResult.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(contractQueryResult.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(contractQueryResult.RentPrice);

            var contractEntity = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, contractQueryResult.RentDueDate, contractQueryResult.ContractStartDate, contractQueryResult.ContractEndDate, id, renterId, tenantId, guarantorId);

            return contractEntity;
        }

        public static ContractWithGuarantorEntity EntityFromModel(this GetContractWithGuarantorForCUD contractForCUDResult)
        {
            if (contractForCUDResult == null)
                return null;

            var id = contractForCUDResult.Id;
            var contractName = contractForCUDResult.ContractName;
            var renterId = contractForCUDResult.RenterId;
            var tenantId = contractForCUDResult.TenantId;
            var guarantorId = contractForCUDResult.GuarantorId;
            var address = new AddressValueObject(contractForCUDResult.Street, contractForCUDResult.Neighborhood, contractForCUDResult.City,
                contractForCUDResult.CEP, contractForCUDResult.State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(contractForCUDResult.PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(contractForCUDResult.RentPrice);

            var contractEntity = new ContractWithGuarantorEntity(contractName, address, propertyRegistrationNumber, rentPrice, contractForCUDResult.RentDueDate, contractForCUDResult.ContractStartDate, contractForCUDResult.ContractEndDate, id, renterId, tenantId, guarantorId);

            return contractEntity;
        }
    }
}

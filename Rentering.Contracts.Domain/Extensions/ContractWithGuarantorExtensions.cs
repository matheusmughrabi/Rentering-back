using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class ContractWithGuarantorExtensions
    {
        public static ContractWithGuarantorEntity EntityFromModel(this GetContractWithGuarantorQueryResult contractQueryResult)
        {
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
    }
}

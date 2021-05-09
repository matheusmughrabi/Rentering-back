using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class ContractPaymentExtensions
    {
        public static ContractPaymentEntity EntityFromModel(this GetContractPaymentQueryResult contractPaymentQueryResult)
        {
            if (contractPaymentQueryResult == null)
                return null;

            var id = contractPaymentQueryResult.Id;
            var contractId = contractPaymentQueryResult.ContractId;
            var month = contractPaymentQueryResult.Month;
            var rentPrice = new PriceValueObject(contractPaymentQueryResult.RentPrice);

            var contractPaymentEntity = new ContractPaymentEntity(contractId, month, rentPrice, id);

            return contractPaymentEntity;
        }
    }
}

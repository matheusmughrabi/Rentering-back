using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
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
            var renterStatus = (e_RenterPaymentStatus) contractPaymentQueryResult.RenterPaymentStatus;
            var tenantStatus = (e_TenantPaymentStatus) contractPaymentQueryResult.TenantPaymentStatus;

            var contractPaymentEntity = new ContractPaymentEntity(contractId, month, rentPrice, id, renterStatus, tenantStatus);

            return contractPaymentEntity;
        }

        public static ContractPaymentEntity EntityFromModel(this GetPaymentForCUD contractPaymentForCUD)
        {
            if (contractPaymentForCUD == null)
                return null;

            var id = contractPaymentForCUD.Id;
            var contractId = contractPaymentForCUD.ContractId;
            var month = contractPaymentForCUD.Month;
            var rentPrice = new PriceValueObject(contractPaymentForCUD.RentPrice);
            var renterStatus = (e_RenterPaymentStatus)contractPaymentForCUD.RenterPaymentStatus;
            var tenantStatus = (e_TenantPaymentStatus)contractPaymentForCUD.TenantPaymentStatus;

            var contractPaymentEntity = new ContractPaymentEntity(contractId, month, rentPrice, id, renterStatus, tenantStatus);

            return contractPaymentEntity;
        }
    }
}

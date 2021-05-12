using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD
{
    public class GetEstateContractForCUD : IGetForCUD<EstateContractEntity>
    {
        public int Id { get; set; }
        public string ContractName { get; set; }
        public int RenterId { get; set; }
        public int TenantId { get; set; }
        public int GuarantorId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public int PropertyRegistrationNumber { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }

        public EstateContractEntity EntityFromModel()
        {
            var id = Id;
            var contractName = ContractName;
            var renterId = RenterId;
            var tenantId = TenantId;
            var guarantorId = GuarantorId;
            var address = new AddressValueObject(Street, Neighborhood, City, CEP, State);
            var propertyRegistrationNumber = new PropertyRegistrationNumberValueObject(PropertyRegistrationNumber);
            var rentPrice = new PriceValueObject(RentPrice);

            var contractEntity = new EstateContractEntity(contractName, address, propertyRegistrationNumber, rentPrice, RentDueDate, ContractStartDate, ContractEndDate, id, renterId, tenantId, guarantorId);

            return contractEntity;
        }
    }

    public class GetPaymentForCUD : IGetForCUD<ContractPaymentEntity>
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public int RenterPaymentStatus { get; set; }
        public int TenantPaymentStatus { get; set; }

        public ContractPaymentEntity EntityFromModel()
        {
            var id = Id;
            var contractId = ContractId;
            var month = Month;
            var rentPrice = new PriceValueObject(RentPrice);
            var renterStatus = (e_RenterPaymentStatus)RenterPaymentStatus;
            var tenantStatus = (e_TenantPaymentStatus)TenantPaymentStatus;

            var contractPaymentEntity = new ContractPaymentEntity(contractId, month, rentPrice, id, renterStatus, tenantStatus);

            return contractPaymentEntity;
        }
    }
}

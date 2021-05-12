using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults
{
    public class GetContractWithGuarantorForCUD
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
    }

    public class GetPaymentForCUD
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public int RenterPaymentStatus { get; set; }
        public int TenantPaymentStatus { get; set; }
    }
}

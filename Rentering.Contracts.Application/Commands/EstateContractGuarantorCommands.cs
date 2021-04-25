using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateEstateContractGuarantorCommand : ICommand
    {
        public CreateEstateContractGuarantorCommand(
            string contractName, 
            int renterId, 
            int renterAccountId, 
            int tenantId, 
            int tenantAccountId, 
            int guarantorId, 
            int guarantorAccountId, 
            string street, 
            string neighborhood, 
            string city, 
            string cep, 
            e_BrazilStates state, 
            int number, 
            decimal rentPrice, 
            DateTime rentDueDate, 
            DateTime contractStartDate, 
            DateTime contractEndDate)
        {
            ContractName = contractName;
            RenterId = renterId;
            RenterAccountId = renterAccountId;
            TenantId = tenantId;
            TenantAccountId = tenantAccountId;
            GuarantorId = guarantorId;
            GuarantorAccountId = guarantorAccountId;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            CEP = cep;
            State = state;
            PropertyRegistrationNumber = number;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;
        }

        public string ContractName { get; set; }
        public int RenterId { get; set; }
        public int RenterAccountId { get; set; }
        public int TenantId { get; set; }
        public int TenantAccountId { get; set; }
        public int GuarantorId { get; set; }
        public int GuarantorAccountId { get; set; }
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
}

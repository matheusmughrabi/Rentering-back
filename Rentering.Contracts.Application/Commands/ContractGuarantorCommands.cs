using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateContractGuarantorCommand : ICommand
    {
        public CreateContractGuarantorCommand(
            string contractName,
            string street, 
            string neighborhood, 
            string city, 
            string cep, 
            e_BrazilStates state, 
            int propertyRegistrationNumber, 
            decimal rentPrice, 
            DateTime rentDueDate, 
            DateTime contractStartDate, 
            DateTime contractEndDate)
        {
            ContractName = contractName;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            CEP = cep;
            State = state;
            PropertyRegistrationNumber = propertyRegistrationNumber;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;
        }

        public string ContractName { get; set; }
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

    public class InviteRenterToParticipate : ICommand
    {
        public InviteRenterToParticipate(int id, int renterId)
        {
            Id = id;
            RenterId = renterId;
        }

        public int Id { get; set; }
        public int RenterId { get; set; }
    }

    public class InviteTenantToParticipate : ICommand
    {
        public InviteTenantToParticipate(int id, int tenantId)
        {
            Id = id;
            TenantId = tenantId;
        }

        public int Id { get; set; }
        public int TenantId { get; set; }
    }

    public class InviteGuarantorToParticipate : ICommand
    {
        public InviteGuarantorToParticipate(int id, int guarantorId)
        {
            Id = id;
            GuarantorId = guarantorId;
        }

        public int Id { get; set; }
        public int GuarantorId { get; set; }
    }

    public class CreateContractPaymentCycleCommand : ICommand
    {
        public CreateContractPaymentCycleCommand(int contractId)
        {
            ContractId = contractId;
        }

        public int ContractId { get; set; }
    }
}

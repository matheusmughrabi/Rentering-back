using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateEstateContractCommand : ICommand
    {
        public CreateEstateContractCommand(
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

        [JsonIgnore]
        public int AccountId { get; set; }
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
    
    public class InviteParticipantCommand : ICommand
    {
        public InviteParticipantCommand(int contractId, int accountId, e_ParticipantRole participantRole)
        {
            ContractId = contractId;
            AccountId = accountId;
            ParticipantRole = participantRole;
        }

        public int ContractId { get; set; }
        public int AccountId { get; set; }
        public e_ParticipantRole ParticipantRole { get; set; }
    }

    public class CreateContractPaymentCycleCommand : ICommand
    {
        public CreateContractPaymentCycleCommand(int contractId)
        {
            ContractId = contractId;
        }

        public int ContractId { get; set; }
    }

    public class ExecutePaymentCommand : ICommand
    {
        public ExecutePaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class AcceptPaymentCommand : ICommand
    {
        public AcceptPaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class RejectPaymentCommand : ICommand
    {
        public RejectPaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class AcceptToParticipateCommand : ICommand
    {
        public AcceptToParticipateCommand(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int AccountId { get; set; }
        public int ContractId { get; set; }
    }

    public class RejectToParticipateCommand : ICommand
    {
        public RejectToParticipateCommand(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int AccountId { get; set; }
        public int ContractId { get; set; }
    }

    public class GetCurrentOwedAmountCommand : ICommand
    {
        public GetCurrentOwedAmountCommand(int contractId)
        {
            ContractId = contractId;
        }

        public int ContractId { get; set; }
    }
}

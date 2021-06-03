using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateEstateContractCommandEF : ICommand
    {
        public CreateEstateContractCommandEF(
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
}

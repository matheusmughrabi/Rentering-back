using Rentering.Common.Shared.Commands;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateContractCommand : Command
    {
        public CreateContractCommand(
            string contractName,
            decimal rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate)
        {
            ContractName = contractName;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;
        }

        [JsonIgnore]
        public int AccountId { get; set; }
        public string ContractName { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
    }
}

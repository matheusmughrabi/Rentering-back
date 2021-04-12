using FluentValidator;
using Rentering.Common.Shared.Commands;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateContractCommand : Notifiable, ICommand
    {
        public CreateContractCommand(string contractName, decimal price, int tentantId)
        {
            ContractName = contractName;
            Price = price;
            TentantId = tentantId;
        }

        public string ContractName { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public int RenterId { get; set; }
        public int TentantId { get; set; }
    }

    public class UpdateRentPriceCommand : Notifiable, ICommand
    {
        public UpdateRentPriceCommand(int id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
    }

    public class DeleteContractCommand : ICommand
    {
        public DeleteContractCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

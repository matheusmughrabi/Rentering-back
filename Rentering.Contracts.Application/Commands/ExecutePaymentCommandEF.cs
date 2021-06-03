using Rentering.Common.Shared.Commands;
using System;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class ExecutePaymentCommandEF : ICommand
    {
        public ExecutePaymentCommandEF(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }
}

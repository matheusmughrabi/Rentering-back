using Rentering.Common.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rentering.Contracts.Application.Commands
{
    public class CreatePaymentCycleCommandEF : ICommand
    {
        public CreatePaymentCycleCommandEF(int contractId)
        {
            ContractId = contractId;
        }

        [JsonIgnore]
        public int CurrentUserId { get; set; }
        public int ContractId { get; set; }
    }
}

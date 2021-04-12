using FluentValidator;
using Rentering.Common.Shared.Commands;
using System;

namespace Rentering.Contracts.Application.Commands
{
    public class CreatePaymentCycleCommand : Notifiable, ICommand
    {
        public CreatePaymentCycleCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class ExecutePaymentCommand : Notifiable, ICommand
    {
        public ExecutePaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class AcceptPaymentCommand : Notifiable, ICommand
    {
        public AcceptPaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }

    public class RejectPaymentCommand : Notifiable, ICommand
    {
        public RejectPaymentCommand(int contractId, DateTime month)
        {
            ContractId = contractId;
            Month = month;
        }

        public int ContractId { get; set; }
        public DateTime Month { get; set; }
    }
}

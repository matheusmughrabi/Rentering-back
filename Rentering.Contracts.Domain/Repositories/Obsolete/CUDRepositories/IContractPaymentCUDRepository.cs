using Rentering.Contracts.Domain.Entities;
using System;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractPaymentCUDRepository
    {
        bool CheckIfContractExists(int contractId);
        bool CheckIfDateIsAlreadyRegistered(int contractId, DateTime month);
        ContractPaymentEntity GetContractPaymentByContractIdAndMonth(int contractId, DateTime month);
        void CreatePaymentAnnucalCycle(ContractPaymentEntity contractPayment);
        void ExecutePayment(ContractPaymentEntity contractPayment);
        void AcceptPayment(ContractPaymentEntity contractPayment);
        void RejectPayment(ContractPaymentEntity contractPayment);
    }
}

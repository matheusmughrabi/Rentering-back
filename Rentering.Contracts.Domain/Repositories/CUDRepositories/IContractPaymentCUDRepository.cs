using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractPaymentCUDRepository
    {
        void CreatePayment(ContractPaymentEntity payment);
        void UpdatePayment(int id, ContractPaymentEntity payment);
        void DeletePaymentt(int id);
    }
}

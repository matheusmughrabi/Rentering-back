namespace Rentering.Contracts.Domain.Repositories.QueryRepositories
{
    public interface IContractWithGuarantorQueryRepository
    {
        bool CheckIfContractNameExists(string contractName);
    }
}

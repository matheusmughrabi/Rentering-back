namespace Rentering.Contracts.Domain.Repositories.UtilRepositories
{
    public interface IContractWithGuarantorUtilRepository
    {
        bool CheckIfContractNameExists(string contractName);
    }
}

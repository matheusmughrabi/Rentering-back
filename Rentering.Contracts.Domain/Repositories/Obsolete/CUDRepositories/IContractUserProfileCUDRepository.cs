using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IContractUserProfileCUDRepository
    {
        bool CheckIfAccountExists(int accountId);
        ContractUserProfileEntity GetContractUserProfileById(int id);
        void CreateContractUserProfile(ContractUserProfileEntity user);
        //void ChangeUsername(int id, ContractUserProfileEntity user);
        void DeleteContractUserProfile(int id);
    }
}

using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Repositories.CUDRepositories
{
    public interface IRenterCUDRepository
    {
        void CreateRenter(RenterEntity renter);
        void UpdateRenter(int id, RenterEntity renter);
        void DeleteRenter(int renterId);
    }
}

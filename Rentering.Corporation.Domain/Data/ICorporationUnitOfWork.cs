using Rentering.Corporation.Domain.Data.Repositories;

namespace Rentering.Corporation.Domain.Data
{
    public interface ICorporationUnitOfWork
    {
        ICorporationCUDRepository CorporationCUDRepository { get; }
        ICorporationQueryRepository CorporationQueryRepository { get; }

        void Save();
    }
}

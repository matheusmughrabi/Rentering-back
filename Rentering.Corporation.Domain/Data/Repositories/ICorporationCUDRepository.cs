using Rentering.Corporation.Domain.Entities;

namespace Rentering.Corporation.Domain.Data.Repositories
{
    public interface ICorporationCUDRepository
    {
        CorporationEntity GetCorporationForCUD(int id);
        CorporationEntity Add(CorporationEntity entity);
    }
}

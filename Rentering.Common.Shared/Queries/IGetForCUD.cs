using Rentering.Common.Shared.Entities;

namespace Rentering.Common.Shared.Queries
{
    public interface IGetForCUD<T> where T : IEntity
    {
        T EntityFromModel();
    }
}

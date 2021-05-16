using Rentering.Common.Shared.Entities;

namespace Rentering.Common.Shared.Data.Repositories
{
    public interface IGenericCUDRepository<T> where T : IEntity
    {
        T Create(T entity);
        T Update(int id, T entity);
        T Delete(int id);
    }
}

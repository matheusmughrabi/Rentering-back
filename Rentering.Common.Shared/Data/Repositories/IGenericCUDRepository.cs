using Rentering.Common.Shared.Entities;

namespace Rentering.Common.Shared.Data.Repositories
{
    public interface IGenericCUDRepository<T> where T : IEntity
    {
        void Create(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}

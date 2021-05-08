using Rentering.Common.Shared.Queries;
using System.Collections.Generic;

namespace Rentering.Common.Shared.Repositories
{
    public interface IGenericQueryRepository<T> where T : IQueryResult
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}

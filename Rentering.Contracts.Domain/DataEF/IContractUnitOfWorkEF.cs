using Rentering.Contracts.Domain.DataEF.Repositories;
using System;

namespace Rentering.Contracts.Domain.DataEF
{
    public interface IContractUnitOfWorkEF : IDisposable
    {
        IEstateContractCUDRepositoryEF EstateContractCUDRepositoryEF { get; }

        void Save();
    }
}

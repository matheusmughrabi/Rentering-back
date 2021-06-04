using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Data.QueryRepositories;
using System;

namespace Rentering.Contracts.Domain.Data
{
    public interface IContractUnitOfWork : IDisposable
    {
        IEstateContractCUDRepository EstateContractCUDRepository { get; }
        IEstateContractQueryRepository EstateContractQueryRepository { get; }
        IAccountContractCUDRepository AccountContractCUDRepository { get; }

        void Save();
    }
}

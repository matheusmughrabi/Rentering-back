using Rentering.Contracts.Domain.Data.Repositories;
using System;

namespace Rentering.Contracts.Domain.Data
{
    public interface IContractUnitOfWork : IDisposable
    {
        IContractCUDRepository ContractCUDRepository { get; }
        IContractQueryRepository ContractQueryRepository { get; }

        void Save();
    }
}

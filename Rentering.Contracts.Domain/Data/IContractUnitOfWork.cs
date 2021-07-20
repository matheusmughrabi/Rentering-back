using Rentering.Contracts.Domain.Data.CUDRepositories;
using Rentering.Contracts.Domain.Data.QueryRepositories;
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

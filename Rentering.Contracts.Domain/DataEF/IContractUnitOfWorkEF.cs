using Rentering.Contracts.Domain.DataEF.CUDRepositories;
using Rentering.Contracts.Domain.DataEF.QueryRepositories;
using System;

namespace Rentering.Contracts.Domain.DataEF
{
    public interface IContractUnitOfWorkEF : IDisposable
    {
        IEstateContractCUDRepositoryEF EstateContractCUDRepositoryEF { get; }
        IEstateContractQueryRepositoryEF EstateContractQueryRepositoryEF { get; }
        IAccountContractCUDRepositoryEF AccountContractCUDRepositoryEF { get; }

        void Save();
    }
}

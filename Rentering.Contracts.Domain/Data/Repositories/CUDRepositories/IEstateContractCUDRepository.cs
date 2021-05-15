﻿using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories
{
    public interface IEstateContractCUDRepository : IGenericCUDRepository<EstateContractEntity>
    {
        EstateContractEntity GetContractForCUD(int id);

        EstateContractEntity InsertTest(EstateContractEntity contract);
    }
}

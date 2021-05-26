﻿using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.DataEF.Repositories
{
    public interface IEstateContractCUDRepositoryEF
    {
        EstateContractEntity GetEstateContractForCUD(int estateContractId);
        bool ContractNameExists(string contractName);
        EstateContractEntity Add(EstateContractEntity estateContractEntity);
        EstateContractEntity Delete(EstateContractEntity estateContractEntity);
        EstateContractEntity Delete(int id);
    }
}

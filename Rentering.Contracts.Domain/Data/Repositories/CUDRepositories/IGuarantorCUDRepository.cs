﻿using Rentering.Common.Shared.Data.Repositories;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories
{
    public interface IGuarantorCUDRepository : IGenericCUDRepository<GuarantorEntity>
    {
        GuarantorEntity GetGuarantorForCUD(int guarantorId);
    }
}

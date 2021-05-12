﻿using Rentering.Accounts.Domain.Entities;
using Rentering.Common.Shared.Data.Repositories;

namespace Rentering.Accounts.Domain.Data.Repositories.CUDRepositories
{
    public interface IAccountCUDRepository : IGenericCUDRepository<AccountEntity>
    {
        AccountEntity GetAccountForCUD(int id);
    }
}


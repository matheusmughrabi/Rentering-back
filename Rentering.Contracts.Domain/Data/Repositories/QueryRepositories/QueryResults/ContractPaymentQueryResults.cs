﻿using Rentering.Common.Shared.Queries;
using System;

namespace Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults
{
    public class GetContractPaymentQueryResult : IQueryResult
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public DateTime Month { get; set; }
        public decimal RentPrice { get; set; }
        public int RenterPaymentStatus { get; set; }
        public int TenantPaymentStatus { get; set; }
    }
}
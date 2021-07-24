using Rentering.Common.Shared.Entities;
using System;

namespace Rentering.Corporation.Domain.Entities
{
    public class MonthlyBalanceEntity : Entity
    {
        public MonthlyBalanceEntity(DateTime month, decimal totalProfit, int corporationId)
        {
            Month = month;
            TotalProfit = totalProfit;
            CorporationId = corporationId;
        }

        public DateTime Month { get; private set; }
        public decimal TotalProfit { get; private set; }
        public int CorporationId { get; private set; }
    }
}

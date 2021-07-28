using Rentering.Common.Shared.Entities;
using System;
using System.Collections.Generic;

namespace Rentering.Corporation.Domain.Entities
{
    public class MonthlyBalanceEntity : Entity
    {
        private List<ParticipantBalanceEntity> _participantBalances;

        public MonthlyBalanceEntity(DateTime month, decimal totalProfit, int corporationId)
        {
            Month = month;
            TotalProfit = totalProfit;
            CorporationId = corporationId;

            _participantBalances = new List<ParticipantBalanceEntity>();
        }

        public DateTime Month { get; private set; }
        public decimal TotalProfit { get; private set; }
        public int CorporationId { get; private set; }
        public IReadOnlyCollection<ParticipantBalanceEntity> ParticipantBalances => _participantBalances.ToArray();

        public void AddParticipantBalance(ParticipantEntity participant)
        {
            var participantBalance = new ParticipantBalanceEntity(participant, this);

            _participantBalances.Add(participantBalance);
        }
    }
}

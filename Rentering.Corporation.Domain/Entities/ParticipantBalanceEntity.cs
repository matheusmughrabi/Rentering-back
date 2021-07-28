using Rentering.Common.Shared.Entities;
using System;

namespace Rentering.Corporation.Domain.Entities
{
    public class ParticipantBalanceEntity : Entity
    {
        protected ParticipantBalanceEntity()
        {
        }

        public ParticipantBalanceEntity(ParticipantEntity participant, MonthlyBalanceEntity monthlyBalance)
        {
            if (participant == null)
                throw new Exception("Participant não pode ser nulo");

            if (monthlyBalance == null)
                throw new Exception("MonthlyBalance não pode ser nulo");

            Participant = participant;
            MonthlyBalance = monthlyBalance;
            ParticipantId = participant.Id;
            MonthlyBalanceId = monthlyBalance.Id;

            CalculateBalance();
        }

        public int ParticipantId { get; private set; }
        public virtual ParticipantEntity Participant { get; private set; }
        public int MonthlyBalanceId { get; private set; }
        public virtual MonthlyBalanceEntity MonthlyBalance { get; private set; }
        public decimal Balance { get; private set; }

        private void CalculateBalance()
        {
            Balance = Participant.SharedPercentage * MonthlyBalance.TotalProfit; 
        }
    }
}

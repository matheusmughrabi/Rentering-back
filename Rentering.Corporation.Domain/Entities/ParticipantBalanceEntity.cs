using Rentering.Common.Shared.Entities;
using Rentering.Corporation.Domain.Enums;
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
            Status = EParticipantBalanceStatus.Pending;
            Description = string.Empty;
            CalculateBalance();
        }

        public int ParticipantId { get; private set; }
        public virtual ParticipantEntity Participant { get; private set; }
        public int MonthlyBalanceId { get; private set; }
        public virtual MonthlyBalanceEntity MonthlyBalance { get; private set; }
        public decimal Balance { get; private set; }
        public string Description { get; private set; }
        public EParticipantBalanceStatus Status { get; set; }

        public void AcceptBalance()
        {
            if (Status == EParticipantBalanceStatus.Accepted)
            {
                AddNotification("Status", "Você já confirmou este mês.");
                return;
            }

            Status = EParticipantBalanceStatus.Accepted;
        }

        public void RejectBalance()
        {
            if (Status == EParticipantBalanceStatus.Rejected)
            {
                AddNotification("Status", "Você já recusou este mês.");
                return;
            }

            Status = EParticipantBalanceStatus.Rejected;
        }

        public void AddDescription(string description)
        {
            Description = description;
        }

        public void RecalculateBalance()
        {
            CalculateBalance();
        }

        private void CalculateBalance()
        {
            Balance = (Participant.SharedPercentage/100) * MonthlyBalance.TotalProfit; 
        }
    }
}

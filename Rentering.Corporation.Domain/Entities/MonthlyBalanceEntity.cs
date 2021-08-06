using Rentering.Common.Shared.Entities;
using Rentering.Corporation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Corporation.Domain.Entities
{
    public class MonthlyBalanceEntity : Entity
    {
        private List<ParticipantBalanceEntity> _participantBalances;

        protected MonthlyBalanceEntity()
        {
        }

        public MonthlyBalanceEntity(DateTime startDate, DateTime endDate, decimal totalProfit, int corporationId)
        {
            StartDate = startDate;
            EndDate = endDate;
            TotalProfit = totalProfit;
            CorporationId = corporationId;
            Status = e_MonthlyBalanceStatus.Pending;

            _participantBalances = new List<ParticipantBalanceEntity>();
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalProfit { get; private set; }
        public int CorporationId { get; private set; }
        public e_MonthlyBalanceStatus Status { get; private set; }
        public IReadOnlyCollection<ParticipantBalanceEntity> ParticipantBalances => _participantBalances.ToArray();

        public void AddParticipantBalance(ParticipantEntity participant)
        {
            var participantBalance = new ParticipantBalanceEntity(participant, this);

            _participantBalances.Add(participantBalance);
        }

        public void Accept(int accountId)
        {
            if (Status == e_MonthlyBalanceStatus.Finished)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês já foi concluído");
                return;
            }

            var participantBalance = _participantBalances.Where(c => c.Participant.AccountId == accountId).FirstOrDefault();

            if (participantBalance == null)
            {
                AddNotification("Balance", "Participant balance não foi encontrado.");
                return;
            }

            participantBalance.AcceptBalance();
            AddNotifications(participantBalance.Notifications);

            bool allParticipantsAccepted = _participantBalances.Any(c => c.Status == e_ParticipantBalanceStatus.Pending || c.Status == e_ParticipantBalanceStatus.Rejected) == false;

            if (allParticipantsAccepted)
                Status = e_MonthlyBalanceStatus.Finished;
        }

        public void Reject(int accountId)
        {
            if (Status == e_MonthlyBalanceStatus.Finished)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês já foi concluído");
                return;
            }

            var participantBalance = _participantBalances.Where(c => c.Participant.AccountId == accountId).FirstOrDefault();

            if (participantBalance == null)
            {
                AddNotification("Balance", "Participant balance não foi encontrado.");
                return;
            }

            participantBalance.RejectBalance();
            AddNotifications(participantBalance.Notifications);
        }

        public void AddDescription(int accountId, string description)
        {
            var participantBalance = _participantBalances.Where(c => c.Participant.AccountId == accountId).FirstOrDefault();

            if (participantBalance == null)
            {
                AddNotification("Balance", "Participant balance não foi encontrado.");
                return;
            }

            participantBalance.AddDescription(description);
            AddNotifications(participantBalance.Notifications);
        }
    }
}

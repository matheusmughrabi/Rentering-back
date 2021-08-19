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
        private List<IncomeEntity> _incomes;

        protected MonthlyBalanceEntity()
        {
        }

        public MonthlyBalanceEntity(DateTime startDate, DateTime endDate, int corporationId)
        {
            StartDate = startDate;
            EndDate = endDate;
            CorporationId = corporationId;
            Status = EMonthlyBalanceStatus.OnGoing;

            _participantBalances = new List<ParticipantBalanceEntity>();
            _incomes = new List<IncomeEntity>();
        }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal TotalProfit { get; private set; }
        public int CorporationId { get; private set; }
        public EMonthlyBalanceStatus Status { get; private set; }
        public IReadOnlyCollection<ParticipantBalanceEntity> ParticipantBalances => _participantBalances.ToArray();
        public IReadOnlyCollection<IncomeEntity> Incomes => _incomes.ToArray();

        public void RegisterIncome(string title, string description, decimal value)
        {
            if (Status != EMonthlyBalanceStatus.OnGoing)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês não está em andamento");
                return;
            }

            var income = new IncomeEntity(title, description, value, this.Id);
            AddNotifications(income.Notifications);

            if (income.Valid)
            {
                _incomes.Add(income);
                TotalProfit += value;

                RecalculateParticipantsBalances();
            }
        }

        public void CloseMonth()
        {
            if (Status != EMonthlyBalanceStatus.OnGoing)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês não está em andamento");
                return;
            }

            Status = EMonthlyBalanceStatus.Pending;
        }

        public void AddParticipantBalance(ParticipantEntity participant)
        {
            var participantBalance = new ParticipantBalanceEntity(participant, this);

            _participantBalances.Add(participantBalance);
        }

        public void Accept(int accountId)
        {
            if (Status == EMonthlyBalanceStatus.Finished)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês já foi concluído");
                return;
            }

            if (Status == EMonthlyBalanceStatus.OnGoing)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês ainda está em andamento.");
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

            bool allParticipantsAccepted = _participantBalances.Any(c => c.Status == EParticipantBalanceStatus.Pending || c.Status == EParticipantBalanceStatus.Rejected) == false;

            if (allParticipantsAccepted)
                Status = EMonthlyBalanceStatus.Finished;
        }

        public void Reject(int accountId)
        {
            if (Status == EMonthlyBalanceStatus.Finished)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês já foi concluído");
                return;
            }

            if (Status == EMonthlyBalanceStatus.OnGoing)
            {
                AddNotification("Status", "Impossível realizar esta ação, pois o mês ainda está em andamento.");
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

        private void RecalculateParticipantsBalances()
        {
            foreach (var participant in _participantBalances)
            {
                participant.RecalculateBalance();
            }
        }
    }
}

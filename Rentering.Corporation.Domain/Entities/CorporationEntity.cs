using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Corporation.Domain.Entities
{
    public class CorporationEntity : Entity
    {
        private List<ParticipantEntity> _participants;
        private List<MonthlyBalanceEntity> _monthlyBalances;

        protected CorporationEntity()
        {
        }

        public CorporationEntity(string name, int adminId, int? id = null) : base(id)
        {
            Name = name;
            AdminId = adminId;

            if (id == null)
                CreateDate = DateTime.Now;

            _participants = new List<ParticipantEntity>();
            _monthlyBalances = new List<MonthlyBalanceEntity>();

            ApplyValidations();
        }

        public string Name { get; private set; }
        public int AdminId { get; private set; }
        public DateTime CreateDate { get; private set; }
        public IReadOnlyCollection<ParticipantEntity> Participants => _participants.ToArray();
        public IReadOnlyCollection<MonthlyBalanceEntity> MonthlyBalances => _monthlyBalances.ToArray();

        public void InviteParticipant(int accountId, decimal sharedPercentage)
        {
            var participantAlreadyInvited = Participants.Any(c => c.AccountId == accountId);

            if (participantAlreadyInvited)
            {
                AddNotification("Perfil", $"Esta conta já faz parte desta corporação.");
                return;
            }

            var participant = new ParticipantEntity(accountId, this.Id, sharedPercentage);

            _participants.Add(participant);
        }

        public void AddMonth(decimal totalProfit)
        {
            var nextMonth = _monthlyBalances.OrderByDescending(c => c.Month)
                .Select(p => p.Month)
                .FirstOrDefault()
                .AddMonths(1);

            var monthAlreadyExists = _monthlyBalances.Any(c => c.Month == nextMonth);

            if (monthAlreadyExists)
            {
                AddNotification("Perfil", $"Este mês já foi adicionado a esta corporação.");
                return;
            }

            var monthlyBalance = new MonthlyBalanceEntity(nextMonth, totalProfit, this.Id);

            _monthlyBalances.Add(monthlyBalance);
        }

        private void ApplyValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Name, 3, "Nome do contrato", "O nome do contrato precisa ter no mínimo 3 letras.")
                .HasMaxLen(Name, 15, "Nome do contrato", "O nome do contrato precisa ter no máximo 15 letras.")
            );
        }
    }
}

using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Common.Shared.Enums;
using Rentering.Corporation.Domain.Enums;
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
                Status = e_CorporationStatus.InProgress;       

            _participants = new List<ParticipantEntity>();
            _monthlyBalances = new List<MonthlyBalanceEntity>();

            ApplyValidations();
        }

        public string Name { get; private set; }
        public int AdminId { get; private set; }
        public e_CorporationStatus Status { get; private set; }
        public IReadOnlyCollection<ParticipantEntity> Participants => _participants.ToArray();
        public IReadOnlyCollection<MonthlyBalanceEntity> MonthlyBalances => _monthlyBalances.ToArray();

        public void InviteParticipant(int accountId, decimal sharedPercentage)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.InProgress };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível convidar novo participante, pois o estado atual da corporação é {Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participantAlreadyInvited = Participants.Any(c => c.AccountId == accountId);

            if (participantAlreadyInvited)
            {
                AddNotification("Perfil", $"Esta conta já faz parte desta corporação.");
                return;
            }

            var participant = new ParticipantEntity(accountId, this.Id, sharedPercentage);

            _participants.Add(participant);
        }

        public void FinishCreation()
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.InProgress };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível finalizar a criação da corporação, pois o estado atual é {Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            if (_participants.Count() == 0)
            {
                AddNotification("Participantes", "A corporação precisa de ao menos um participante para ser finalizada.");
                return;
            }

            Status = e_CorporationStatus.WaitingParticipants;
        }

        public void AcceptToParticipate(int participantId)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.WaitingParticipants };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível aceitar participação na corporação, pois o estado atual é {Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participant = _participants.Where(c => c.Id == participantId).FirstOrDefault();

            if (participant == null)
            {
                AddNotification("Participante", "O participante informado não faz parte desta corporação.");
                return;
            }

            participant.AcceptToParticipate();
            AddNotifications(participant.Notifications);

            bool pendingInvitations = _participants.Any(c => c.InvitationStatus == e_InvitationStatus.Invited);

            if (Status == e_CorporationStatus.WaitingParticipants && pendingInvitations == false)
                Status = e_CorporationStatus.ReadyForActivation;
        }

        public void RejectToParticipate(int participantId)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.WaitingParticipants };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível recusar participação na corporação, pois o estado atual é {Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participant = _participants.Where(c => c.Id == participantId).FirstOrDefault();

            if (participant == null)
            {
                AddNotification("Participante", "O participante informado não faz parte desta corporação.");
                return;
            }

            participant.RejectToParticipate();
            AddNotifications(participant.Notifications);

            var hasParticipant = _participants.Any(p => p.InvitationStatus != e_InvitationStatus.Rejected);
            if (hasParticipant == false)
            {
                Status = e_CorporationStatus.InProgress;
                return;
            }

            bool pendingInvitations = _participants.Any(c => c.InvitationStatus == e_InvitationStatus.Invited);

            if (Status == e_CorporationStatus.WaitingParticipants && pendingInvitations == false)
            {
                Status = e_CorporationStatus.ReadyForActivation;
                return;
            }
        }

        public void ActivateCorporation()
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.ReadyForActivation };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível ativar a corporação, pois o estado atual é {Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            Status = e_CorporationStatus.Active;

            foreach (var participant in _participants.ToList())
            {
                if (participant.InvitationStatus == e_InvitationStatus.Rejected)
                    _participants.Remove(participant);
            }
        }

        public void AddMonth(DateTime startDate, DateTime endDate)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.Active };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível adicionar mês, pois o estado atual da corporação é{Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var monthlyBalance = new MonthlyBalanceEntity(startDate, endDate, this.Id);

            foreach (var participant in _participants.Where(c => c.InvitationStatus == e_InvitationStatus.Accepted))
            {
                monthlyBalance.AddParticipantBalance(participant);
            }

            _monthlyBalances.Add(monthlyBalance);
        }

        public void RegisterIncomeInMonth(int monthlyBalanceId, string title, string description, decimal value)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.Active };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível adicionar mês, pois o estado atual da corporação é{Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var monthlyBalance = _monthlyBalances.Where(c => c.Id == monthlyBalanceId).FirstOrDefault();

            if (monthlyBalance == null)
            {
                AddNotification("Mês", $"O mês informado não percente a esta corporação.");
                return;
            }

            monthlyBalance.RegisterIncome(title, description, value);
            AddNotifications(monthlyBalance.Notifications);
        }

        public void CloseSpecifiedMonth(int monthlyBalanceId)
        {
            e_CorporationStatus[] acceptedStates = { e_CorporationStatus.Active };
            bool isAllowed = IsProcessAllowed(acceptedStates, $"Impossível adicionar mês, pois o estado atual da corporação é{Status.ToDescription()}.");

            if (isAllowed == false)
                return;

            var monthlyBalance = _monthlyBalances.Where(c => c.Id == monthlyBalanceId).FirstOrDefault();

            if (monthlyBalance == null)
            {
                AddNotification("Mês", $"O mês informado não percente a esta corporação.");
                return;
            }

            monthlyBalance.CloseMonth();
            AddNotifications(monthlyBalance.Notifications);
        }

        public void AcceptParticipantBalance(int monthlyBalanceId, int accountId)
        {
            var monthlyBalance = _monthlyBalances.Where(c => c.Id == monthlyBalanceId).FirstOrDefault();

            if (monthlyBalance == null)
            {
                AddNotification("Mês", $"O mês informado não percente a esta corporação.");
                return;
            }

            monthlyBalance.Accept(accountId);
            AddNotifications(monthlyBalance.Notifications);
        }

        public void RejectParticipantBalance(int monthlyBalanceId, int accountId)
        {
            var monthlyBalance = _monthlyBalances.Where(c => c.Id == monthlyBalanceId).FirstOrDefault();

            if (monthlyBalance == null)
            {
                AddNotification("Mês", $"O mês informado não percente a esta corporação.");
                return;
            }

            monthlyBalance.Reject(accountId);
            AddNotifications(monthlyBalance.Notifications);
        }

        public void AddParticipantDescriptionToMonth(int monthlyBalanceId, int accountId, string description)
        {
            var monthlyBalance = _monthlyBalances.Where(c => c.Id == monthlyBalanceId).FirstOrDefault();

            if (monthlyBalance == null)
            {
                AddNotification("Mês", $"O mês informado não percente a esta corporação.");
                return;
            }

            monthlyBalance.AddDescription(accountId, description);
            AddNotifications(monthlyBalance.Notifications);
        }

        private void ApplyValidations()
        {
            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(Name, 3, "Nome do contrato", "O nome do contrato precisa ter no mínimo 3 letras.")
                .HasMaxLen(Name, 15, "Nome do contrato", "O nome do contrato precisa ter no máximo 15 letras.")
            );
        }

        private bool IsProcessAllowed(e_CorporationStatus[] allowedStatuses, string message)
        {
            var isAllowed = true;

            if (allowedStatuses.Contains(Status) == false)
            {
                AddNotification("Ação negada", message);
                isAllowed = false;
            }

            return isAllowed;
        }
    }
}

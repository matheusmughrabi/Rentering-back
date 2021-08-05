using FluentValidator.Validation;
using Rentering.Common.Shared.Entities;
using Rentering.Common.Shared.Enums;
using Rentering.Common.Shared.Extensions;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Rentering.Contracts.Domain.Entities
{
    public class ContractEntity : Entity
    {
        private List<AccountContractsEntity> _participants;
        private List<ContractPaymentEntity> _payments;

        protected ContractEntity()
        {
        }

        public ContractEntity(
            string contractName,
            PriceValueObject rentPrice,
            DateTime rentDueDate,
            DateTime contractStartDate,
            DateTime contractEndDate,
            int? id = null) : base(id)
        {
            ContractName = contractName;
            RentPrice = rentPrice;
            RentDueDate = rentDueDate;
            ContractStartDate = contractStartDate;
            ContractEndDate = contractEndDate;

            _participants = new List<AccountContractsEntity>();
            _payments = new List<ContractPaymentEntity>();

            ApplyValidations();
        }

        public string ContractName { get; private set; }
        public e_ContractState ContractState { get; private set; } = e_ContractState.NotEnoughParticipants;
        [Required]
        public PriceValueObject RentPrice { get; private set; }
        public DateTime RentDueDate { get; private set; }
        public DateTime ContractStartDate { get; private set; }
        public DateTime ContractEndDate { get; private set; }
        public IReadOnlyCollection<AccountContractsEntity> Participants => _participants.ToArray();
        public IReadOnlyCollection<ContractPaymentEntity> Payments => _payments.ToArray();

        public void InviteParticipant(int accountId, e_ParticipantRole participantRole)
        {
            e_ContractState[] acceptedStates = { e_ContractState.NotEnoughParticipants, e_ContractState.WaitingParticipantsAccept };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível convidar novo participante, pois o estado atual do contrato é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            var isParticipantAlreadyInThisRole = Participants.Any(c => c.AccountId == accountId && c.ParticipantRole == participantRole);

            if (isParticipantAlreadyInThisRole)
            {
                AddNotification("Perfil", $"Esta conta já faz parte deste contrato como { participantRole.ToDescription() }.");
                return;
            }

            if (_participants.Count() == 0)
            {
                var accountContractsEntity = new AccountContractsEntity(accountId, Id, participantRole, e_ParticipantStatus.Accepted);
                _participants.Add(accountContractsEntity);
            }
            else
            {
                var accountContractsEntity = new AccountContractsEntity(accountId, Id, participantRole);
                _participants.Add(accountContractsEntity);
            }

            bool contractHasMinimunPreRequisites = _participants
                .Any(c => c.ParticipantRole == e_ParticipantRole.Payer) 
                && _participants.Any(c => c.ParticipantRole == e_ParticipantRole.Receiver);

            if (contractHasMinimunPreRequisites)
                ContractState = e_ContractState.WaitingParticipantsAccept;
        }

        public void RemoveParticipant(int accountId)
        {
            e_ContractState[] acceptedStates = { e_ContractState.NotEnoughParticipants, e_ContractState.WaitingParticipantsAccept };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível convidar novo participante, pois o estado atual do contrato é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participantToRemove = _participants.Where(c => c.AccountId == accountId).FirstOrDefault();

            if (participantToRemove == null)
                AddNotification("Participante", "O participante informado não faz parte deste contrato.");

            _participants.Remove(participantToRemove);
        }

        public void ActivateContract()
        {
            if (ContractState != e_ContractState.ReadyForActivation)
            {
                AddNotification("Estado do contrato", $"O contrato ainda não está pronto para ativação, pois está {ContractState.ToDescription()}");
                return;
            }

            ContractState = e_ContractState.Active;
            CreatePaymentCycle();
        }

        public void AcceptToParticipate(int accountContractId)
        {
            e_ContractState[] acceptedStates = { e_ContractState.NotEnoughParticipants, e_ContractState.WaitingParticipantsAccept };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível aceitar participação, pois o estado atual do contrato é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participant = _participants.Where(c => c.Id == accountContractId).FirstOrDefault();

            if (participant == null)
                AddNotification("Participante", "O participante informado não faz parte deste contrato.");

            participant.AcceptToParticipate();

            bool pendingInvitations = _participants.Any(c => c.Status == e_ParticipantStatus.Pending || c.Status == e_ParticipantStatus.Rejected);

            if (ContractState == e_ContractState.WaitingParticipantsAccept && pendingInvitations == false)
                ContractState = e_ContractState.ReadyForActivation;
        }

        public void RejectToParticipate(int accountContractId)
        {
            e_ContractState[] acceptedStates = { e_ContractState.NotEnoughParticipants, e_ContractState.WaitingParticipantsAccept };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível aceitar participação, pois o estado atual do contrato é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            var participant = _participants.Where(c => c.Id == accountContractId).FirstOrDefault();

            if (participant == null)
                AddNotification("Participante", "O participante informado não faz parte deste contrato.");

            participant.RejectToParticipate();
        }

        public void UpdateRentPrice(PriceValueObject rentPrice)
        {
            e_ContractState[] acceptedStates = { e_ContractState.NotEnoughParticipants, e_ContractState.WaitingParticipantsAccept };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível atualizar o preço do contrato, pois o estado atual é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            if (rentPrice.Price < 0)
            {
                AddNotifications(rentPrice.Notifications);
                return;
            }

            RentPrice = rentPrice;
        }

        private void CreatePaymentCycle()
        {
            e_ContractState[] acceptedStates = { e_ContractState.Active };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível criar um ciclo de pagamento, pois o estado atual do contrato é {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return;

            var monthSpan = (ContractEndDate - ContractStartDate).GetMonths();

            if (monthSpan < 0)
            {
                AddNotification("Período em meses", "O período mínimo para criação de um ciclo de pagamento é 1 mês");
                return;
            }

            for (int i = 0; i < monthSpan; i++)
            {
                var monthToBeAdded = ContractStartDate.AddMonths(i);

                if (_payments.Any(c => c.Month == monthToBeAdded))
                {
                    AddNotification("Período em meses", $"{monthToBeAdded} já está registrado no ciclo de pagamento do contrato");
                    continue;
                }

                _payments.Add(new ContractPaymentEntity(Id, monthToBeAdded, new PriceValueObject(RentPrice.Price)));
            }
        }

        public ContractPaymentEntity ExecutePayment(DateTime month)
        {
            e_ContractState[] acceptedStates = { e_ContractState.Active};
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível executar pagamento, pois o estado atual do contrato {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return null;

            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("Período em meses", $"{month} não faz parte do ciclo de pagamentos deste contrato.");
                return null;
            }

            payment.ExecutePayment();
            return payment;
        }

        public ContractPaymentEntity AcceptPayment(DateTime month)
        {
            e_ContractState[] acceptedStates = { e_ContractState.Active };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível aceitar pagamento, pois o estado atual do contrato {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return null;

            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("Período em meses", $"{month} is not registered in payment cycle of this contract");
                return null;
            }

            payment.AcceptPayment();
            return payment;
        }

        public ContractPaymentEntity RejectPayment(DateTime month)
        {
            e_ContractState[] acceptedStates = { e_ContractState.Active };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível recusar pagamento, pois o estado atual do contrato {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return null;

            var payment = Payments.Where(p => p.Month.Year == month.Year && p.Month.Month == month.Month).FirstOrDefault();

            if (payment == null)
            {
                AddNotification("Período em meses", $"{month} is not registered in payment cycle of this contract");
                return null;
            }

            payment.RejectPayment();
            return payment;
        }

        public decimal CurrentOwedAmount()
        {
            e_ContractState[] acceptedStates = { e_ContractState.Active };
            bool isAllowed = IsProcessAllowedInCurrentContractState(acceptedStates, $"Impossível calcular o valor devido atualmente, pois o estado atual do contrato {ContractState.ToDescription()}.");

            if (isAllowed == false)
                return 0M;

            var currentPayment = Payments.OrderBy(c => c.Month)
                .Where(c => c.PayerPaymentStatus == e_PayerPaymentStatus.None)
                .FirstOrDefault();

            if (currentPayment == null)
            {
                AddNotification("Pagamentos", $"Não existem pagamentos em aberto.");
                return 0M;
            }

            var currentOwedAmount = currentPayment.CalculateOwedAmount(RentDueDate);
            return currentOwedAmount;
        }

        private void ApplyValidations()
        {
            var monthSpan = (ContractEndDate - ContractStartDate).GetMonths();

            AddNotifications(new ValidationContract()
                .Requires()
                .HasMinLen(ContractName, 3, "Nome do contrato", "O nome do contrato precisa ter no mínimo 3 letras.")
                .HasMaxLen(ContractName, 40, "Nome do contrato", "O nome do contrato precisa ter no máximo 40 letras.")
                .IsGreaterOrEqualsThan(monthSpan, 1, "Período em meses", "O período do contrato deve ser de pelo menos 1 mês.")
            );

            AddNotifications(RentPrice.Notifications);
        }

        private bool IsProcessAllowedInCurrentContractState(e_ContractState[] contractStatesAllowed, string message)
        {
            var isAllowed = true;

            if (contractStatesAllowed.Contains(ContractState) == false)
            {
                AddNotification("Estado do contrato", message);
                isAllowed = false;
            }

            return isAllowed;
        }
    }
}

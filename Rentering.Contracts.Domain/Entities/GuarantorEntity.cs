using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Rentering.Contracts.Domain.Entities
{
    public class GuarantorEntity : Entity
    {
        protected GuarantorEntity()
        {
        }

        public GuarantorEntity(
            int contractId,
            NameValueObject name, 
            string nationality, 
            string ocupation, 
            e_MaritalStatus maritalStatus, 
            IdentityRGValueObject identityRG, 
            CPFValueObject cpf, 
            AddressValueObject address, 
            NameValueObject spouseName = null, 
            string spouseNationality = null, 
            string spouseOcupation = null, 
            IdentityRGValueObject spouseIdentityRG = null,
            CPFValueObject spouseCPF = null,
            e_ContractParticipantStatus? guarantorStatus = null,
            int? id = null) : base(id)
        {
            ContractId = contractId;
            Name = name;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Address = address;
            SpouseName = spouseName;
            SpouseNationality = spouseNationality;
            SpouseOcupation = spouseOcupation;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;

            if (guarantorStatus == null)
                GuarantorStatus = e_ContractParticipantStatus.None;
            else
                GuarantorStatus = (e_ContractParticipantStatus)guarantorStatus;
        }

        public int ContractId { get; private set; }
        public e_ContractParticipantStatus GuarantorStatus { get; private set; }
        [Required]
        public NameValueObject Name { get; private set; }
        public string Nationality { get; private set; }
        public string Ocupation { get; private set; }
        public e_MaritalStatus MaritalStatus { get; private set; }
        [Required]
        public IdentityRGValueObject IdentityRG { get; private set; }
        [Required]
        public CPFValueObject CPF { get; private set; }
        [Required]
        public AddressValueObject Address { get; private set; }
        public NameValueObject SpouseName { get; private set; }
        public string SpouseNationality { get; private set; }
        public string SpouseOcupation { get; private set; }
        public IdentityRGValueObject SpouseIdentityRG { get; private set; }
        public CPFValueObject SpouseCPF { get; private set; }

        public void AcceptToParticipate()
        {
            if (GuarantorStatus == e_ContractParticipantStatus.Aceito)
            {
                AddNotification("GuarantorStatus", "The status is already accepted");
                return;
            }

            GuarantorStatus = e_ContractParticipantStatus.Aceito;
        }

        public void RefuseToParticipate()
        {
            if (GuarantorStatus == e_ContractParticipantStatus.Recusado)
            {
                AddNotification("GuarantorStatus", "The status is already refused");
                return;
            }

            GuarantorStatus = e_ContractParticipantStatus.Recusado;
        }

        public void UpdateGuarantorStatusToAwaiting()
        {
            if (GuarantorStatus == e_ContractParticipantStatus.Pendente)
            {
                AddNotification("GuarantorStatus", "The status is already awaiting");
                return;
            }

            GuarantorStatus = e_ContractParticipantStatus.Pendente;
        }
    }
}

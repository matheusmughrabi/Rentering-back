using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public class TenantEntity : Entity
    {
        public TenantEntity(
            int accountId,
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
            CPFValueObject spouseCPF = null)
        {
            AccountId = accountId;
            TenantStatus = e_ContractParticipantStatus.Pendente;
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
        }

        public int AccountId { get; private set; }
        public e_ContractParticipantStatus TenantStatus { get; private set; }
        public NameValueObject Name { get; private set; }
        public string Nationality { get; private set; }
        public string Ocupation { get; private set; }
        public e_MaritalStatus MaritalStatus { get; private set; }
        public IdentityRGValueObject IdentityRG { get; private set; }
        public CPFValueObject CPF { get; private set; }
        public AddressValueObject Address { get; private set; }
        public NameValueObject SpouseName { get; private set; }
        public string SpouseNationality { get; private set; }
        public string SpouseOcupation { get; private set; }
        public IdentityRGValueObject SpouseIdentityRG { get; private set; }
        public CPFValueObject SpouseCPF { get; private set; }

        public void AcceptToParticipate()
        {
            if (TenantStatus == e_ContractParticipantStatus.Aceito)
            {
                AddNotification("TenantStatus", "The status is already accepted");
                return;
            }

            TenantStatus = e_ContractParticipantStatus.Aceito;
        }

        public void RefuseToParticipate()
        {
            if (TenantStatus == e_ContractParticipantStatus.Recusado)
            {
                AddNotification("TenantStatus", "The status is already refused");
                return;
            }

            TenantStatus = e_ContractParticipantStatus.Recusado;
        }

        public void UpdateTenantStatusToAwaiting()
        {
            if (TenantStatus == e_ContractParticipantStatus.Pendente)
            {
                AddNotification("TenantStatus", "The status is already awaiting");
                return;
            }

            TenantStatus = e_ContractParticipantStatus.Pendente;
        }
    }
}

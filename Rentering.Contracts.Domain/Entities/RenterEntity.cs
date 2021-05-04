using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public class RenterEntity : Entity
    {
        public RenterEntity(
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
            IdentityRGValueObject spouseIdentityRG = null,
            CPFValueObject spouseCPF = null,
            e_ContractParticipantStatus? renterStatus = null,
            int? id = null)
        {
            AccountId = accountId;            
            Name = name;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Address = address;
            SpouseName = spouseName;
            SpouseNationality = spouseNationality;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;

            if (id != null)
                AssignId((int)id);

            if (renterStatus == null)
                RenterStatus = e_ContractParticipantStatus.None;
            else
                RenterStatus = (e_ContractParticipantStatus)renterStatus;
        }

        public int AccountId { get; private set; }
        public e_ContractParticipantStatus RenterStatus { get; private set; }
        public NameValueObject Name { get; private set; }
        public string Nationality { get; private set; }
        public string Ocupation { get; private set; }
        public e_MaritalStatus MaritalStatus { get; private set; }
        public IdentityRGValueObject IdentityRG { get; private set; }
        public CPFValueObject CPF { get; private set; }
        public AddressValueObject Address { get; private set; }
        public NameValueObject SpouseName { get; private set; }
        public string SpouseNationality { get; private set; }
        public IdentityRGValueObject SpouseIdentityRG { get; private set; }
        public CPFValueObject SpouseCPF { get; private set; }

        public void AcceptToParticipate()
        {
            if (RenterStatus == e_ContractParticipantStatus.Aceito)
            {
                AddNotification("RenterStatus", "The status is already accepted");
                return;
            }               

            RenterStatus = e_ContractParticipantStatus.Aceito;
        }

        public void RefuseToParticipate()
        {
            if (RenterStatus == e_ContractParticipantStatus.Recusado)
            {
                AddNotification("RenterStatus", "The status is already refused");
                return;
            }

            RenterStatus = e_ContractParticipantStatus.Recusado;
        }

        public void UpdateRenterStatusToAwaiting()
        {
            if (RenterStatus == e_ContractParticipantStatus.Pendente)
            {
                AddNotification("RenterStatus", "The status is already awaiting");
                return;
            }

            RenterStatus = e_ContractParticipantStatus.Pendente;
        }
    }
}

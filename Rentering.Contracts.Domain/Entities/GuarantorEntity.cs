using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Entities
{
    public class GuarantorEntity : Entity
    {
        public GuarantorEntity(
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
            GuarantorStatus = e_ContractParticipantStatus.Pendente;
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
        public e_ContractParticipantStatus GuarantorStatus { get; private set; }
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
    }
}

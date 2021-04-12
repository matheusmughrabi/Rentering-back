using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Entities
{
    public class RenterEntity
    {
        public RenterEntity(
            NameValueObject name, 
            string nationality, 
            string ocupation, 
            e_MaritalStatus maritalStatus, 
            IdentityRGValueObject identityRG,
            CPFValueObject cpf, 
            AddressValueObject address,
            NameValueObject spouseFirstName, 
            string spouseNationality,
            IdentityRGValueObject spouseIdentityRG, 
            CPFValueObject spouseCPF,
            int accountId)
        {
            Name = name;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Address = address;
            SpouseFirstName = spouseFirstName;
            SpouseNationality = spouseNationality;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;
            AccountId = accountId;
        }

        public NameValueObject Name { get; private set; }
        public string Nationality { get; private set; }
        public string Ocupation { get; private set; }
        public e_MaritalStatus MaritalStatus { get; private set; }
        public IdentityRGValueObject IdentityRG { get; private set; }
        public CPFValueObject CPF { get; private set; }
        public AddressValueObject Address { get; private set; }
        public NameValueObject SpouseFirstName { get; private set; }
        public string SpouseNationality { get; private set; }
        public IdentityRGValueObject SpouseIdentityRG { get; private set; }
        public CPFValueObject SpouseCPF { get; private set; }
        public int AccountId { get; private set; }
    }
}

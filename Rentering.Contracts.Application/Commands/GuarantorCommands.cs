﻿using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateGuarantorCommand : ICommand
    {
        public CreateGuarantorCommand(
            int contractId,
            string firstName,
            string lastName,
            string nationality,
            string ocupation,
            e_MaritalStatus maritalStatus,
            string identityRG,
            string cpf,
            string street,
            string neighborhood,
            string city,
            string cep,
            e_BrazilStates state,
            string spouseFirstName,
            string spouseLastName,
            string spouseNationality,
            string spouseOcupation,
            string spouseIdentityRG,
            string spouseCPF)
        {
            ContractId = contractId;
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            CEP = cep;
            State = state;
            SpouseFirstName = spouseFirstName;
            SpouseLastName = spouseLastName;
            SpouseOcupation = spouseOcupation;
            SpouseNationality = spouseNationality;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;
        }

        public int ContractId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Ocupation { get; set; }
        public e_MaritalStatus MaritalStatus { get; set; }
        public string IdentityRG { get; set; }
        public string CPF { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseNationality { get; set; }
        public string SpouseOcupation { get; set; }
        public string SpouseIdentityRG { get; set; }
        public string SpouseCPF { get; set; }
    }

    public class UpdateGuarantorCommand : ICommand
    {
        public UpdateGuarantorCommand(
            int id,
            int contractId,
            string firstName,
            string lastName,
            string nationality,
            string ocupation,
            e_MaritalStatus maritalStatus,
            string identityRG,
            string cpf,
            string street,
            string neighborhood,
            string city,
            string cep,
            e_BrazilStates state,
            string spouseFirstName,
            string spouseLastName,
            string spouseNationality,
            string spouseOcupation,
            string spouseIdentityRG,
            string spouseCPF)
        {
            Id = id;
            ContractId = contractId;
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Street = street;
            Neighborhood = neighborhood;
            City = city;
            CEP = cep;
            State = state;
            SpouseFirstName = spouseFirstName;
            SpouseLastName = spouseLastName;
            SpouseOcupation = spouseOcupation;
            SpouseNationality = spouseNationality;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;
        }

        public int Id { get; set; }
        public int ContractId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Ocupation { get; set; }
        public e_MaritalStatus MaritalStatus { get; set; }
        public string IdentityRG { get; set; }
        public string CPF { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseNationality { get; set; }
        public string SpouseOcupation { get; set; }
        public string SpouseIdentityRG { get; set; }
        public string SpouseCPF { get; set; }
    }

    public class DeleteGuarantorCommand : ICommand
    {
        public DeleteGuarantorCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

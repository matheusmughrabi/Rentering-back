using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateRenterCommand : ICommand
    {
        public CreateRenterCommand(
            int accountId, 
            string firstName, 
            string lastName, 
            string nationality, 
            string ocupation, 
            e_MaritalStatus maritalStatus, 
            string identityRG, 
            string cpf, 
            string street, 
            string bairro, 
            string cidade, 
            string cep, 
            string estado, 
            string spouseFirstName, 
            string spouseLastName, 
            string spouseNationality, 
            string spouseIdentityRG,
            string spouseCPF)
        {
            AccountId = accountId;
            FirstName = firstName;
            LastName = lastName;
            Nationality = nationality;
            Ocupation = ocupation;
            MaritalStatus = maritalStatus;
            IdentityRG = identityRG;
            CPF = cpf;
            Street = street;
            Bairro = bairro;
            Cidade = cidade;
            CEP = cep;
            Estado = estado;
            SpouseFirstName = spouseFirstName;
            SpouseLastName = spouseLastName;
            SpouseNationality = spouseNationality;
            SpouseIdentityRG = spouseIdentityRG;
            SpouseCPF = spouseCPF;
        }

        public int AccountId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Nationality { get; private set; }
        public string Ocupation { get; private set; }
        public e_MaritalStatus MaritalStatus { get; private set; }
        public string IdentityRG { get; private set; }
        public string CPF { get; private set; }
        public string Street { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string CEP { get; private set; }
        public string Estado { get; private set; }
        public string SpouseFirstName { get; private set; }
        public string SpouseLastName { get; private set; }
        public string SpouseNationality { get; private set; }
        public string SpouseIdentityRG { get; private set; }
        public string SpouseCPF { get; private set; }
    }
}

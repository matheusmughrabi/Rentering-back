using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using System.Text.Json.Serialization;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateRenterCommand : ICommand
    {
        public CreateRenterCommand(
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

        [JsonIgnore]
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Ocupation { get; set; }
        public e_MaritalStatus MaritalStatus { get; set; }
        public string IdentityRG { get; set; }
        public string CPF { get; set; }
        public string Street { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string SpouseFirstName { get; set; }
        public string SpouseLastName { get; set; }
        public string SpouseNationality { get; set; }
        public string SpouseIdentityRG { get; set; }
        public string SpouseCPF { get; set; }
    }

    public class DeleteRenterCommand : ICommand
    {
        public DeleteRenterCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

}

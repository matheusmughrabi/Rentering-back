using Rentering.Common.Shared.Commands;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Application.Commands
{
    public class CreateRenterCommand : ICommand
    {
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

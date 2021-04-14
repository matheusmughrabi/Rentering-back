using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults
{
    public class GetRenterQueryResult
    {
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
}

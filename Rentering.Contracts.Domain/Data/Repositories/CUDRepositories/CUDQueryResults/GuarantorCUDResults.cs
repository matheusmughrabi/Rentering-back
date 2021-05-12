using Rentering.Contracts.Domain.Enums;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults
{
    public class GetGuarantorForCUD
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public e_ContractParticipantStatus Status { get; set; }
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
}

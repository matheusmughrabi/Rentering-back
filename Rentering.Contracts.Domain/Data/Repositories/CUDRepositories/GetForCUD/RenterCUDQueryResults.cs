using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD
{
    public class GetRenterForCUD : IGetForCUD<RenterEntity>
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
        public string SpouseIdentityRG { get; set; }
        public string SpouseCPF { get; set; }

        public RenterEntity EntityFromModel()
        {
            var id = Id;
            var accountId = AccountId;
            var status = Status;
            var name = new NameValueObject(FirstName, LastName);
            var nationality = Nationality;
            var ocupation = Ocupation;
            var maritalStatus = MaritalStatus;
            var identityRG = new IdentityRGValueObject(IdentityRG);
            var cpf = new CPFValueObject(CPF);
            var address = new AddressValueObject(Street, Neighborhood, City, CEP, State);
            var spouseName = new NameValueObject(SpouseFirstName, SpouseLastName, false, false);
            var spouseNationality = SpouseNationality;
            var spouseIdentityRG = new IdentityRGValueObject(SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(SpouseCPF);

            var renterEntity = new RenterEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, cpf, address, spouseName, spouseNationality, spouseIdentityRG, spouseCPF, status, id);

            return renterEntity;
        }
    }
}

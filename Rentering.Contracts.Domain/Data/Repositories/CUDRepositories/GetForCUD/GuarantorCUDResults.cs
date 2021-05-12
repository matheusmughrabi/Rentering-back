using Rentering.Common.Shared.Queries;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Enums;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD
{
    public class GetGuarantorForCUD : IGetForCUD<GuarantorEntity>
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

        public GuarantorEntity EntityFromModel()
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
            var spouseOcupation = SpouseOcupation;
            var spouseIdentityRG = new IdentityRGValueObject(SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(SpouseCPF, false);

            var guarantorEntity = new GuarantorEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, cpf, address, spouseName, spouseNationality, spouseOcupation, spouseIdentityRG, spouseCPF, status, id);

            return guarantorEntity;
        }
    }
}

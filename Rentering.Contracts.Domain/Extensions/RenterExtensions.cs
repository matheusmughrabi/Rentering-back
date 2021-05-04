using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class RenterExtensions
    {
        public static RenterEntity EntityFromModel(this GetRenterQueryResult renterQueryResult)
        {
            var id = renterQueryResult.Id;
            var accountId = renterQueryResult.AccountId;
            var status = renterQueryResult.Status;
            var name = new NameValueObject(renterQueryResult.FirstName, renterQueryResult.LastName);
            var nationality = renterQueryResult.Nationality;
            var ocupation = renterQueryResult.Ocupation;
            var maritalStatus = renterQueryResult.MaritalStatus;
            var identityRG = new IdentityRGValueObject(renterQueryResult.IdentityRG);
            var CPF = new CPFValueObject(renterQueryResult.CPF);
            var address = new AddressValueObject(renterQueryResult.Street, renterQueryResult.Neighborhood, renterQueryResult.City, renterQueryResult.CEP,
                renterQueryResult.State);
            var spouseName = new NameValueObject(renterQueryResult.SpouseFirstName, renterQueryResult.SpouseLastName, false, false);
            var spouseNationality = renterQueryResult.SpouseNationality;
            var spouseIdentityRG = new IdentityRGValueObject(renterQueryResult.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(renterQueryResult.SpouseCPF);

            var renterEntity = new RenterEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseIdentityRG, spouseCPF, status, id);

            return renterEntity;
        }
    }
}

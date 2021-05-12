using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class RenterExtensions
    {
        public static RenterEntity EntityFromModel(this GetRenterQueryResult renterQueryResult)
        {
            if (renterQueryResult == null)
                return null;

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

        public static RenterEntity EntityFromModel(this GetRenterForCUD getRenterForCUD)
        {
            if (getRenterForCUD == null)
                return null;

            var id = getRenterForCUD.Id;
            var accountId = getRenterForCUD.AccountId;
            var status = getRenterForCUD.Status;
            var name = new NameValueObject(getRenterForCUD.FirstName, getRenterForCUD.LastName);
            var nationality = getRenterForCUD.Nationality;
            var ocupation = getRenterForCUD.Ocupation;
            var maritalStatus = getRenterForCUD.MaritalStatus;
            var identityRG = new IdentityRGValueObject(getRenterForCUD.IdentityRG);
            var CPF = new CPFValueObject(getRenterForCUD.CPF);
            var address = new AddressValueObject(getRenterForCUD.Street, getRenterForCUD.Neighborhood, getRenterForCUD.City, getRenterForCUD.CEP,
                getRenterForCUD.State);
            var spouseName = new NameValueObject(getRenterForCUD.SpouseFirstName, getRenterForCUD.SpouseLastName, false, false);
            var spouseNationality = getRenterForCUD.SpouseNationality;
            var spouseIdentityRG = new IdentityRGValueObject(getRenterForCUD.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(getRenterForCUD.SpouseCPF);

            var renterEntity = new RenterEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseIdentityRG, spouseCPF, status, id);

            return renterEntity;
        }
    }
}

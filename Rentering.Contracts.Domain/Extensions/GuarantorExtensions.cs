using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class GuarantorExtensions
    {
        public static GuarantorEntity EntityFromModel(this GetGuarantorQueryResult guarantorQueryResult)
        {
            if (guarantorQueryResult == null)
                return null;

            var id = guarantorQueryResult.Id;
            var accountId = guarantorQueryResult.AccountId;
            var status = guarantorQueryResult.Status;
            var name = new NameValueObject(guarantorQueryResult.FirstName, guarantorQueryResult.LastName);
            var nationality = guarantorQueryResult.Nationality;
            var ocupation = guarantorQueryResult.Ocupation;
            var maritalStatus = guarantorQueryResult.MaritalStatus;
            var identityRG = new IdentityRGValueObject(guarantorQueryResult.IdentityRG);
            var CPF = new CPFValueObject(guarantorQueryResult.CPF);
            var address = new AddressValueObject(guarantorQueryResult.Street, guarantorQueryResult.Neighborhood, guarantorQueryResult.City, guarantorQueryResult.CEP, guarantorQueryResult.State);
            var spouseName = new NameValueObject(guarantorQueryResult.SpouseFirstName, guarantorQueryResult.SpouseLastName, false, false);
            var spouseNationality = guarantorQueryResult.SpouseNationality;
            var spouseOcupation = guarantorQueryResult.SpouseOcupation;
            var spouseIdentityRG = new IdentityRGValueObject(guarantorQueryResult.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(guarantorQueryResult.SpouseCPF, false);

            var guarantorEntity = new GuarantorEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseOcupation, spouseIdentityRG, spouseCPF, status, id);

            return guarantorEntity;
        }
    }
}

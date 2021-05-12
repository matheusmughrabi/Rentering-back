using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
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

        public static GuarantorEntity EntityFromModel(this GetGuarantorForCUD getGuarantorForCUD)
        {
            if (getGuarantorForCUD == null)
                return null;

            var id = getGuarantorForCUD.Id;
            var accountId = getGuarantorForCUD.AccountId;
            var status = getGuarantorForCUD.Status;
            var name = new NameValueObject(getGuarantorForCUD.FirstName, getGuarantorForCUD.LastName);
            var nationality = getGuarantorForCUD.Nationality;
            var ocupation = getGuarantorForCUD.Ocupation;
            var maritalStatus = getGuarantorForCUD.MaritalStatus;
            var identityRG = new IdentityRGValueObject(getGuarantorForCUD.IdentityRG);
            var CPF = new CPFValueObject(getGuarantorForCUD.CPF);
            var address = new AddressValueObject(getGuarantorForCUD.Street, getGuarantorForCUD.Neighborhood, getGuarantorForCUD.City, getGuarantorForCUD.CEP, getGuarantorForCUD.State);
            var spouseName = new NameValueObject(getGuarantorForCUD.SpouseFirstName, getGuarantorForCUD.SpouseLastName, false, false);
            var spouseNationality = getGuarantorForCUD.SpouseNationality;
            var spouseOcupation = getGuarantorForCUD.SpouseOcupation;
            var spouseIdentityRG = new IdentityRGValueObject(getGuarantorForCUD.SpouseIdentityRG, false);
            var spouseCPF = new CPFValueObject(getGuarantorForCUD.SpouseCPF, false);

            var guarantorEntity = new GuarantorEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseOcupation, spouseIdentityRG, spouseCPF, status, id);

            return guarantorEntity;
        }
    }
}

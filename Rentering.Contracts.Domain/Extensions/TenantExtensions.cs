using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.CUDQueryResults;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.Domain.ValueObjects;

namespace Rentering.Contracts.Domain.Extensions
{
    public static class TenantExtensions
    {
        public static TenantEntity EntityFromModel(this GetTenantQueryResult tenantQueryResult)
        {
            if (tenantQueryResult == null)
                return null;

            var id = tenantQueryResult.Id;
            var accountId = tenantQueryResult.AccountId;
            var status = tenantQueryResult.Status;
            var name = new NameValueObject(tenantQueryResult.FirstName, tenantQueryResult.LastName);
            var nationality = tenantQueryResult.Nationality;
            var ocupation = tenantQueryResult.Ocupation;
            var maritalStatus = tenantQueryResult.MaritalStatus;
            var identityRG = new IdentityRGValueObject(tenantQueryResult.IdentityRG);
            var CPF = new CPFValueObject(tenantQueryResult.CPF);
            var address = new AddressValueObject(tenantQueryResult.Street, tenantQueryResult.Neighborhood, tenantQueryResult.City, tenantQueryResult.CEP,
                tenantQueryResult.State);
            var spouseName = new NameValueObject(tenantQueryResult.SpouseFirstName, tenantQueryResult.SpouseLastName, false, false);
            var spouseNationality = tenantQueryResult.SpouseNationality;
            var spouseOcupation = tenantQueryResult.SpouseOcupation;
            var spouseIdentityRG = new IdentityRGValueObject(tenantQueryResult.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(tenantQueryResult.SpouseCPF);

            var tenantEntity = new TenantEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseOcupation, spouseIdentityRG, spouseCPF, status, id);

            return tenantEntity;
        }

        public static TenantEntity EntityFromModel(this GetTenantForCUD getTenantForCUD)
        {
            if (getTenantForCUD == null)
                return null;

            var id = getTenantForCUD.Id;
            var accountId = getTenantForCUD.AccountId;
            var status = getTenantForCUD.Status;
            var name = new NameValueObject(getTenantForCUD.FirstName, getTenantForCUD.LastName);
            var nationality = getTenantForCUD.Nationality;
            var ocupation = getTenantForCUD.Ocupation;
            var maritalStatus = getTenantForCUD.MaritalStatus;
            var identityRG = new IdentityRGValueObject(getTenantForCUD.IdentityRG);
            var CPF = new CPFValueObject(getTenantForCUD.CPF);
            var address = new AddressValueObject(getTenantForCUD.Street, getTenantForCUD.Neighborhood, getTenantForCUD.City, getTenantForCUD.CEP,
                getTenantForCUD.State);
            var spouseName = new NameValueObject(getTenantForCUD.SpouseFirstName, getTenantForCUD.SpouseLastName, false, false);
            var spouseNationality = getTenantForCUD.SpouseNationality;
            var spouseOcupation = getTenantForCUD.SpouseOcupation;
            var spouseIdentityRG = new IdentityRGValueObject(getTenantForCUD.SpouseIdentityRG);
            var spouseCPF = new CPFValueObject(getTenantForCUD.SpouseCPF);

            var tenantEntity = new TenantEntity(accountId, name, nationality, ocupation, maritalStatus, identityRG, CPF, address, spouseName, spouseNationality, spouseOcupation, spouseIdentityRG, spouseCPF, status, id);

            return tenantEntity;
        }
    }
}

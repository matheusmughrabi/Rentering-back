﻿using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories;
using Rentering.Contracts.Domain.Data.Repositories.CUDRepositories.GetForCUD;
using Rentering.Contracts.Domain.Entities;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.CUDRepositories
{
    public class EstateContractCUDRepository : IEstateContractCUDRepository
    {
        private readonly RenteringDataContext _context;

        public EstateContractCUDRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public EstateContractEntity GetContractForCUD(int id)
        {
            var contractSql = @"SELECT * FROM EstateContracts WHERE Id = @Id";

            var participantsSql = @"SELECT * FROM AccountContracts WHERE ContractId = @ContractId";
            var renterSql = @"SELECT * FROM Renters WHERE ContractId = @ContractId";
            var tenantSql = @"SELECT * FROM Tenants WHERE ContractId = @ContractId";
            var guarantorSql = @"SELECT * FROM Guarantors WHERE ContractId = @ContractId";
            var paymentSql = @"SELECT * FROM ContractPayments WHERE ContractId = @ContractId";

            var contractFromDb = _context.Connection.Query<GetEstateContractForCUD>(
                   contractSql,
                   new { Id = id }).FirstOrDefault();

            if (contractFromDb == null)
                return null;

            var participantsFromDb = _context.Connection.Query<GetAccountContractsForCUD>(
                  participantsSql,
                  new { ContractId = id });

            var rentersFromDb = _context.Connection.Query<GetRenterForCUD>(
                   renterSql,
                   new { ContractId = id });

            var tenantsFromDb = _context.Connection.Query<GetTenantForCUD>(
                   tenantSql,
                   new { ContractId = id });

            var guarantorsFromDb = _context.Connection.Query<GetGuarantorForCUD>(
                   guarantorSql,
                   new { ContractId = id });

            var paymentsFromDb = _context.Connection.Query<GetPaymentForCUD>(
                   paymentSql,
                   new { ContractId = id });

            var contractEntity = contractFromDb.EntityFromModel();

            var participantEntities = participantsFromDb?.Select(c => c.EntityFromModel()).ToList();
            var renterEntities = rentersFromDb?.Select(c => c.EntityFromModel()).ToList();
            var tenantEntities = tenantsFromDb?.Select(c => c.EntityFromModel()).ToList();
            var guarantorEntities = guarantorsFromDb?.Select(c => c.EntityFromModel()).ToList();
            var paymentEntities = paymentsFromDb?.Select(c => c.EntityFromModel()).ToList();

            contractEntity.IncludeParticipants(participantEntities);
            contractEntity.IncludeRenters(renterEntities);
            contractEntity.IncludeTenants(tenantEntities);
            contractEntity.IncludeGuarantors(guarantorEntities);
            contractEntity.IncludeContractPayments(paymentEntities);

            return contractEntity;
        }

        public EstateContractEntity Create(EstateContractEntity contract)
        {
            if (contract == null)
                return null;

            string sql = @"INSERT INTO [EstateContracts](
                                [ContractName],
							    [Street],
							    [Neighborhood],
							    [City],
							    [CEP],
							    [State],
							    [PropertyRegistrationNumber],
							    [RentPrice],
							    [RentDueDate],
							    [ContractStartDate],
							    [ContractEndDate])
                        OUTPUT INSERTED.*
                        VALUES(
                                @ContractName,
								@Street,
								@Neighborhood,
								@City,
								@CEP,
								@State,
								@PropertyRegistrationNumber,
								@RentPrice,
								@RentDueDate,
								@ContractStartDate,
								@ContractEndDate);";

            var createdContractFromDb = _context.Connection.QuerySingle<GetEstateContractForCUD>(sql,
                    new
                    {
                        contract.ContractName,
                        contract.Address.Street,
                        contract.Address.Neighborhood,
                        contract.Address.City,
                        contract.Address.CEP,
                        contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        contract.RentDueDate,
                        contract.ContractStartDate,
                        contract.ContractEndDate
                    },
                    _context.Transaction);

            var createdContractEntity = createdContractFromDb.EntityFromModel();

            return createdContractEntity;
        }

        public EstateContractEntity Update(int id, EstateContractEntity contract)
        {
            if (contract == null)
                return null;

            var sql = @"UPDATE 
							EstateContracts
						SET
							[ContractName] = @ContractName,
							[Street] = @Street,
							[Neighborhood] = @Neighborhood,
							[City] = @City,
							[CEP] = @CEP,
							[State] = @State,
							[PropertyRegistrationNumber] = @PropertyRegistrationNumber,
							[RentPrice] = @RentPrice,
							[RentDueDate] = @RentDueDate,
							[ContractStartDate] = @ContractStartDate,
							[ContractEndDate] = @ContractEndDate
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var updatedContractFromDb = _context.Connection.QuerySingle<GetEstateContractForCUD>(sql,
                    new
                    {
                        contract.Id,
                        contract.ContractName,
                        contract.Address.Street,
                        contract.Address.Neighborhood,
                        contract.Address.City,
                        contract.Address.CEP,
                        contract.Address.State,
                        PropertyRegistrationNumber = contract.PropertyRegistrationNumber.Number,
                        RentPrice = contract.RentPrice.Price,
                        contract.RentDueDate,
                        contract.ContractStartDate,
                        contract.ContractEndDate
                    },
                    _context.Transaction);

            var updatedContractEntity = updatedContractFromDb.EntityFromModel();

            return updatedContractEntity;
        }

        public EstateContractEntity Delete(int id)
        {
            var sql = @"DELETE 
                        FROM 
							EstateContracts
                        OUTPUT INSERTED.*
						WHERE 
							Id = @Id;";

            var deletedContractFromDb = _context.Connection.QuerySingle<GetEstateContractForCUD>(sql,
                   new{ Id = id },
                   _context.Transaction);

            var deletedContractEntity = deletedContractFromDb.EntityFromModel();

            return deletedContractEntity;
        }
    }
}

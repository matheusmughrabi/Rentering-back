using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class EstateContractQueryRepository : IEstateContractQueryRepository
    {
        private readonly RenteringDataContext _context;

        public EstateContractQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public bool CheckIfContractNameExists(string contractName)
        {
            var sql = @"SELECT CASE WHEN EXISTS (
		                        SELECT [Id]
		                        FROM [EstateContracts]
		                        WHERE [ContractName] = @ContractName
	                        )
	                        THEN CAST(1 AS BIT)
	                        ELSE CAST(0 AS BIT)
                            END;";

            var contractNameExists = _context.Connection.Query<bool>(
                    sql,
                    new { ContractName = contractName }).FirstOrDefault();

            return contractNameExists;
        }

        public IEnumerable<GetEstateContractQueryResult> GetAll()
        {
            var sql = @"SELECT 
							Id, 
                            ContractName, 
                            RenterId, 
                            TenantId, 
                            GuarantorId, 
                            Street, 
                            Neighborhood, 
                            City, 
                            CEP, 
                            State, 
                            PropertyRegistrationNumber, 
                            RentPrice, 
                            RentDueDate, 
                            ContractStartDate, 
                            ContractEndDate
						FROM 
							EstateContracts;";

            var contractsFromDb = _context.Connection.Query<GetEstateContractQueryResult>(sql);

            return contractsFromDb;
        }

        public GetEstateContractQueryResult GetById(int id)
        {
            // TODO - Corrigir RenterId, TenantId e GuarantorId
            var sql = @"SELECT
							Id, 
                            ContractName, 
                            RenterId, 
                            TenantId, 
                            GuarantorId, 
                            Street, 
                            Neighborhood, 
                            City, 
                            CEP, 
                            State, 
                            PropertyRegistrationNumber, 
                            RentPrice, 
                            RentDueDate, 
                            ContractStartDate, 
                            ContractEndDate
						FROM 
							EstateContracts
						WHERE 
							[Id] = @Id;";

            var contractFromDb = _context.Connection.Query<GetEstateContractQueryResult>(
                    sql,
                    new { Id = id }).FirstOrDefault();

            return contractFromDb;
        }

        public GetCurrentUserContract GetContract(int contractId)
        {
            var query = @"
                SELECT * FROM EstateContracts WHERE Id = @ContractId
                SELECT A.Username, AC.ParticipantRole, AC.Status
                FROM AccountContracts AS AC
                INNER JOIN Accounts AS A ON A.Id = AC.AccountId
                WHERE AC.ContractId = @ContractId;
                ";

            var result = _context.Connection.QueryMultiple(query, new { ContractId = contractId });

            var contractQuey = new GetCurrentUserContract();
            try
            {
                contractQuey = result.Read<GetCurrentUserContract>().Single();
                contractQuey.Participants = result.Read<ContractParticipants>().ToList();

                return contractQuey;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}

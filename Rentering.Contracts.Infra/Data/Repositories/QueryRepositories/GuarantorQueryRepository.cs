﻿using Dapper;
using Rentering.Common.Infra;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories;
using Rentering.Contracts.Domain.Data.Repositories.QueryRepositories.QueryResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rentering.Contracts.Infra.Data.Repositories.QueryRepositories
{
    public class GuarantorQueryRepository : IGuarantorQueryRepository
    {
        private readonly RenteringDataContext _context;

        public GuarantorQueryRepository(RenteringDataContext context)
        {
            _context = context;
        }

        public IEnumerable<GetGuarantorQueryResult> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public GetGuarantorQueryResult GetById(int id)
        {
            var sql = @"SELECT
							Id,
                            ContractId,
                            Status,
                            FirstName,
                            LastName,
                            Nationality,
                            Ocupation,
                            MaritalStatus,
                            IdentityRG,
                            CPF,
                            Street,
                            SpouseFirstName,
                            SpouseLastName,
                            SpouseNationality,
                            SpouseOcupation,
                            SpouseIdentityRG,
                            SpouseCPF
						FROM 
							Guarantors
						WHERE 
							[Id] = @Id;";

            var guarantorFromDb = _context.Connection.Query<GetGuarantorQueryResult>(
                   sql,
                   new { Id = id }).FirstOrDefault();

            return guarantorFromDb;
        }
    }
}
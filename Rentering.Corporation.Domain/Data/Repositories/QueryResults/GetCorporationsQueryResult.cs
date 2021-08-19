using Rentering.Common.Shared.Data.Interfaces;
using System;

namespace Rentering.Corporation.Domain.Data.Repositories.QueryResults
{
    public class GetCorporationsQueryResult : IDataResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Admin { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

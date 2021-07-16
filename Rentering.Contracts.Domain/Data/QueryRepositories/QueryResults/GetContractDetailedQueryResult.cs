using Rentering.Contracts.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Rentering.Contracts.Domain.Data.QueryRepositories.QueryResults
{
    public class GetContractDetailedQueryResult
    {
        public GetContractDetailedQueryResult()
        {
            Renters = new List<Renters>();
            Tenants = new List<Tenants>();
            Guarantors = new List<Guarantors>();
        }

        public int Id { get; set; }
        public string ContractName { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string CEP { get; set; }
        public e_BrazilStates State { get; set; }
        public int PropertyRegistrationNumber { get; set; }
        public decimal RentPrice { get; set; }
        public DateTime RentDueDate { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public List<Renters> Renters { get; set; }
        public List<Tenants> Tenants { get; set; }
        public List<Guarantors> Guarantors { get; set; }
    }

    public class Renters
    {
        public Renters(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get;  set; }
        public string LastName { get;  set; }
    }

    public class Tenants
    {
        public Tenants(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Guarantors
    {
        public Guarantors(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

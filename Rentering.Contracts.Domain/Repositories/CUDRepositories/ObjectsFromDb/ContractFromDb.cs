namespace Rentering.Contracts.Domain.Repositories.CUDRepositories.ObjectsFromDb
{
    public class ContractFromDb
    {
        public ContractFromDb(string contractName, decimal rentPrice, int renterId, int tenantId)
        {
            ContractName = contractName;
            RentPrice = rentPrice;
            RenterId = renterId;
            TenantId = tenantId;
        }

        public string ContractName { get; set; }
        public decimal RentPrice { get; set; }
        public int RenterId { get; set; }
        public int TenantId { get; set; }
    }
}

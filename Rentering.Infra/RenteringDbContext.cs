using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Entities;
using Rentering.Contracts.Domain.Entities;
using Rentering.Infra.Accounts.Mappings;
using Rentering.Infra.Contracts.Mappings;

namespace Rentering.Infra
{
    public class RenteringDbContext : DbContext
    {
        public RenteringDbContext(DbContextOptions<RenteringDbContext> options) : base(options)
        {
        }

        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<ContractEntity> Contract { get; set; }
        public DbSet<ContractPaymentEntity> ContractPayment { get; set; }
        public DbSet<AccountContractsEntity> AccountContracts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());

            modelBuilder.ApplyConfiguration(new ContractMapping());
            modelBuilder.ApplyConfiguration(new AccountContractsMapping());
            modelBuilder.ApplyConfiguration(new ContractPaymentMapping());       

            base.OnModelCreating(modelBuilder);
        }
    }
}

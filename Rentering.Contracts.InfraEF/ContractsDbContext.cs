using Microsoft.EntityFrameworkCore;
using Rentering.Contracts.Domain.Entities;
using Rentering.Contracts.InfraEF.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentering.Contracts.InfraEF
{
    public class ContractsDbContext : DbContext
    {
        public ContractsDbContext(DbContextOptions<ContractsDbContext> options) : base(options)
        {
        }

        public DbSet<EstateContractEntity> Contract { get; set; }
        public DbSet<RenterEntity> Renter { get; set; }
        public DbSet<TenantEntity> Tenant { get; set; }
        public DbSet<GuarantorEntity> Guarantor { get; set; }
        public DbSet<ContractPaymentEntity> ContractPayment { get; set; }
        public DbSet<AccountContractsEntity> AccountContracts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EstateContractMapping());
            modelBuilder.ApplyConfiguration(new AccountContractsMapping());
            modelBuilder.ApplyConfiguration(new RenterMapping());
            modelBuilder.ApplyConfiguration(new TenantMapping());
            modelBuilder.ApplyConfiguration(new GuarantorMapping());
            modelBuilder.ApplyConfiguration(new ContractPaymentMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}

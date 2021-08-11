using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Entities;
using Rentering.Common.Shared.Entities;
using Rentering.Contracts.Domain.Entities;
using Rentering.Corporation.Domain.Entities;
using Rentering.Infra.Accounts.Mappings;
using Rentering.Infra.Contracts.Mappings;
using Rentering.Infra.Corporations.Mappings;
using System;
using System.Linq;

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

        public DbSet<CorporationEntity> Corporation { get; set; }
        public DbSet<ParticipantEntity> Participant { get; set; }
        public DbSet<MonthlyBalanceEntity> MonthlyBalance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());

            modelBuilder.ApplyConfiguration(new ContractMapping());
            modelBuilder.ApplyConfiguration(new AccountContractsMapping());
            modelBuilder.ApplyConfiguration(new ContractPaymentMapping());

            modelBuilder.ApplyConfiguration(new CorporationMapping());
            modelBuilder.ApplyConfiguration(new ParticipantsMapping());
            modelBuilder.ApplyConfiguration(new MonthlyBalanceMapping());
            modelBuilder.ApplyConfiguration(new ParticipantBalanceMapping());
            modelBuilder.ApplyConfiguration(new IncomeMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                                    .Where(x => x.Entity is Entity &&
                                    (x.State == EntityState.Added));

            foreach (var entity in selectedEntityList)
            {

                ((Entity)entity.Entity).CreateDate = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}

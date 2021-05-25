using Microsoft.EntityFrameworkCore;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.InfraEF.Mappings;

namespace Rentering.Accounts.InfraEF
{
    public class AccountsDbContext : DbContext
    {
        public AccountsDbContext(DbContextOptions<AccountsDbContext> options) : base(options)
        {
        }

        public DbSet<AccountEntity> Account { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}

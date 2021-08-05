using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Accounts.Domain.Entities;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Infra.Corporations.Mappings
{
    public class CorporationMapping : IEntityTypeConfiguration<CorporationEntity>
    {
        public void Configure(EntityTypeBuilder<CorporationEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.HasOne<AccountEntity>()
                .WithMany()
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.CreateDate)
                .IsRequired()
                .HasColumnType("Date");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(c => c.MonthlyBalances)
                .WithOne()
                .HasForeignKey(p => p.CorporationId);

            builder.HasMany(c => c.Participants)
                .WithOne(u => u.Corporation)
                .HasForeignKey(p => p.CorporationId);

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Corporation");
        }
    }
}

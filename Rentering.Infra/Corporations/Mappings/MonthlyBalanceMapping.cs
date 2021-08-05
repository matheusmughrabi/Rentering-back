using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Infra.Corporations.Mappings
{
    public class MonthlyBalanceMapping : IEntityTypeConfiguration<MonthlyBalanceEntity>
    {
        public void Configure(EntityTypeBuilder<MonthlyBalanceEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Month)
                .IsRequired()
                .HasColumnType("Date");

            builder.Property(c => c.TotalProfit)
                .IsRequired()
                .HasColumnType("decimal(19, 5)");

            builder.Property(c => c.CorporationId)
                .IsRequired()
                .HasColumnType("int");

            builder.HasMany(c => c.ParticipantBalances)
                .WithOne(u => u.MonthlyBalance)
                .HasForeignKey(p => p.MonthlyBalanceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Corporation_MonthlyBalance");
        }
    }
}

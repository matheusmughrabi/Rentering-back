using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Infra.Corporations.Mappings
{
    public class IncomeMapping : IEntityTypeConfiguration<IncomeEntity>
    {
        public void Configure(EntityTypeBuilder<IncomeEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasColumnType("nvarchar(20)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Value)
                .IsRequired()
                .HasColumnType("decimal(19, 5)");

            builder.Property(c => c.MonthlyBalanceId)
                .IsRequired()
                .HasColumnType("int");

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Corporation_Incomes");
        }
    }
}

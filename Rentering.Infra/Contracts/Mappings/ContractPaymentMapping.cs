using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Infra.Contracts.Mappings
{
    public class ContractPaymentMapping : IEntityTypeConfiguration<ContractPaymentEntity>
    {
        public void Configure(EntityTypeBuilder<ContractPaymentEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Month)
                .IsRequired()
                .HasColumnType("Date");

            builder.OwnsOne(c => c.RentPrice, p =>
            {
                p.Property(u => u.Price)
                    .IsRequired()
                    .HasColumnName("Price")
                    .HasColumnType("decimal(19,5)");

                p.Ignore(u => u.Notifications);
            });

            builder.Property(c => c.RenterPaymentStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.TenantPaymentStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.Ignore(c => c.Notifications);

            builder.ToTable("ContractPayments");
        }
    }
}

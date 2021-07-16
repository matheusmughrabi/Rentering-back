using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Contracts.Domain.Entities;
using System;

namespace Rentering.Infra.Contracts.Mappings
{
    public class EstateContractMapping : IEntityTypeConfiguration<EstateContractEntity>
    {
        public void Configure(EntityTypeBuilder<EstateContractEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.OwnsOne(c => c.RentPrice, p =>
            {
                p.Property(u => u.Price)
                    .IsRequired()
                    .HasColumnName("Price")
                    .HasColumnType("decimal(19,5)");

                p.Ignore(u => u.Notifications);
            });

            builder.Property(c => c.RentDueDate)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.ContractStartDate)
               .IsRequired()
               .HasColumnType("date");

            builder.Property(c => c.ContractEndDate)
               .IsRequired()
               .HasColumnType("date");

            builder.HasMany(c => c.Participants)
                .WithOne(u => u.EstateContract)
                .HasForeignKey(p => p.ContractId);

            builder.HasMany(c => c.Payments)
                .WithOne()
                .HasForeignKey(p => p.ContractId);

            builder.Ignore(c => c.Notifications);

            builder.ToTable("EstateContracts");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Contracts.Domain.Entities;
using System;

namespace Rentering.Contracts.InfraEF.Mappings
{
    public class EstateContractMapping : IEntityTypeConfiguration<EstateContractEntity>
    {
        public void Configure(EntityTypeBuilder<EstateContractEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractName)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.OwnsOne(c => c.Address, p =>
            {
                p.Property(u => u.Street)
                    .IsRequired()
                    .HasColumnName("Street")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.Neighborhood)
                    .IsRequired()
                    .HasColumnName("Neighborhood")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.City)
                    .IsRequired()
                    .HasColumnName("City")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.CEP)
                    .IsRequired()
                    .HasColumnName("CEP")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.State)
                    .IsRequired()
                    .HasColumnName("State")
                    .HasColumnType("int");

                p.Ignore(u => u.Notifications);
            });

            builder.OwnsOne(c => c.PropertyRegistrationNumber, p =>
            {
                p.Property(u => u.Number)
                    .IsRequired()
                    .HasColumnName("Number")
                    .HasColumnType("int");

                p.Ignore(u => u.Notifications);
            });

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

            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Participants)
                .WithOne(u => u.EstateContract)
                .HasForeignKey(p => p.ContractId);

            builder.HasMany(c => c.Renters)
                .WithOne()
                .HasForeignKey(p => p.ContractId);

            builder.HasMany(c => c.Tenants)
                .WithOne()
                .HasForeignKey(p => p.ContractId);

            builder.HasMany(c => c.Guarantors)
                .WithOne()
                .HasForeignKey(p => p.ContractId);

            builder.HasMany(c => c.Payments)
                .WithOne()
                .HasForeignKey(p => p.ContractId);

            builder.Ignore(c => c.Notifications);

            builder.ToTable("EstateContracts");
        }
    }
}

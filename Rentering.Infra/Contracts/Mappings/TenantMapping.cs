using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Contracts.Domain.Entities;
using System;

namespace Rentering.Infra.Contracts.Mappings
{
    public class TenantMapping : IEntityTypeConfiguration<TenantEntity>
    {
        public void Configure(EntityTypeBuilder<TenantEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContractId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.TenantStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.OwnsOne(c => c.Name, p =>
            {
                p.Property(u => u.FirstName)
                    .IsRequired()
                    .HasColumnName("FirstName")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.LastName)
                    .IsRequired()
                    .HasColumnName("LastName")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.Property(c => c.Nationality)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.Ocupation)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.MaritalStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.OwnsOne(c => c.IdentityRG, p =>
            {
                p.Property(u => u.IdentityRG)
                    .IsRequired()
                    .HasColumnName("IdentityRG")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.OwnsOne(c => c.CPF, p =>
            {
                p.Property(u => u.CPF)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

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

            builder.OwnsOne(c => c.SpouseName, p =>
            {
                p.Property(u => u.FirstName)
                    .IsRequired()
                    .HasColumnName("SpouseFirstName")
                    .HasColumnType("nvarchar(100)");

                p.Property(u => u.LastName)
                    .IsRequired()
                    .HasColumnName("SpouseLastName")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.Property(c => c.SpouseNationality)
                .HasColumnType("nvarchar(100)");

            builder.Property(c => c.SpouseOcupation)
                .HasColumnType("nvarchar(100)");

            builder.OwnsOne(c => c.SpouseIdentityRG, p =>
            {
                p.Property(u => u.IdentityRG)
                    .IsRequired()
                    .HasColumnName("SpouseIdentityRG")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.OwnsOne(c => c.SpouseCPF, p =>
            {
                p.Property(u => u.CPF)
                    .IsRequired()
                    .HasColumnName("SpouseCPF")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Tenants");
        }
    }
}

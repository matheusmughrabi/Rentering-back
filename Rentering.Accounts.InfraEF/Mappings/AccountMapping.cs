using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Accounts.Domain.Entities;
using Rentering.Accounts.Domain.ValueObjects;

namespace Rentering.Accounts.InfraEF.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Email, p =>
            {
                p.Property(u => u.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.OwnsOne(c => c.Username, p =>
            {
                p.Property(u => u.Username)
                    .IsRequired()
                    .HasColumnName("Username")
                    .HasColumnType("nvarchar(100)");

                p.Ignore(u => u.Notifications);
            });

            builder.OwnsOne(c => c.Password, cm =>
            {
                cm.Property(u => u.Password)
                    .IsRequired()
                    .HasColumnName("Password")
                    .HasColumnType("nvarchar(100)");

                cm.Ignore(u => u.Notifications);
            });

            builder.Property(c => c.Role)
                .IsRequired()
                .HasColumnType("int");

            builder.Ignore(c => c.Notifications);

            builder.Ignore(c => c.Token);
        }
    }
}



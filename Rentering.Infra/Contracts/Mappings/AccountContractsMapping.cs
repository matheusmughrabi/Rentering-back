using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Accounts.Domain.Entities;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Infra.Contracts.Mappings
{
    public class AccountContractsMapping : IEntityTypeConfiguration<AccountContractsEntity>
    {
        public void Configure(EntityTypeBuilder<AccountContractsEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.AccountId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.ContractId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.ParticipantRole)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne<AccountEntity>()
               .WithMany()
               .HasForeignKey(c => c.AccountId);

            builder.Ignore(c => c.Notifications);

            builder.ToTable("AccountContracts");
        }
    }
}

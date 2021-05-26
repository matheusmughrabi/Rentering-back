using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Contracts.Domain.Entities;

namespace Rentering.Contracts.InfraEF.Mappings
{
    class AccountContractsMapping : IEntityTypeConfiguration<AccountContractsEntity>
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

            builder.Ignore(c => c.Notifications);

            builder.ToTable("AccountContracts");
        }
    }
}

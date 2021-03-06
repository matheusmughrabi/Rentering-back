using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Accounts.Domain.Entities;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Infra.Corporations.Mappings
{
    public class ParticipantsMapping : IEntityTypeConfiguration<ParticipantEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipantEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.AccountId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.CorporationId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.InvitationStatus)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.SharedPercentage)
                .IsRequired()
                .HasColumnType("decimal(19, 5)");

            builder.HasOne<AccountEntity>()
                .WithMany()
                .HasForeignKey(c => c.AccountId);

            builder.HasMany(c => c.ParticipantBalances)
                .WithOne(u => u.Participant)
                .HasForeignKey(p => p.ParticipantId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Corporation_Participants");
        }
    }
}

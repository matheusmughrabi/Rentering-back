using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.Infra.Corporations.Mappings
{
    public class ParticipantBalanceMapping : IEntityTypeConfiguration<ParticipantBalanceEntity>
    {
        public void Configure(EntityTypeBuilder<ParticipantBalanceEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ParticipantId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.MonthlyBalanceId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Balance)
                .IsRequired()
                .HasColumnType("decimal(19, 5)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("nvarchar(100)");

            builder.Ignore(c => c.Notifications);

            builder.ToTable("Corporation_ParticipantBalance");
        }
    }
}

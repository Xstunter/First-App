using Board.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Host.Data.EntityConfigurations
{
    public class HistoryEntityConfiguration
        : IEntityTypeConfiguration<HistoryEntity>
    {
        public void Configure(EntityTypeBuilder<HistoryEntity> builder)
        {
            builder.ToTable("History");

            builder.Property(ci => ci.HistoryId)
                .UseHiLo("history_hilo")
                .IsRequired();

            builder.Property(ci => ci.Type)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Date)
                .IsRequired(true);

            builder.HasOne(ci => ci.Board)
                .WithMany(ci => ci.Histories)
                .HasForeignKey(ci => ci.BoardId)
                .IsRequired(true);
        }
    }
}

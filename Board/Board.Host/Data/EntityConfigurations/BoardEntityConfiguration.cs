using Board.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Host.Data.EntityConfigurations
{
    public class BoardEntityConfiguration
        : IEntityTypeConfiguration<BoardEntity>
    {
        public void Configure(EntityTypeBuilder<BoardEntity> builder)
        {
            builder.ToTable("Board");

            builder.Property(ci => ci.BoardId)
                .UseHiLo("board_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Description)
                .IsRequired(true)
                .HasMaxLength(500);

            builder.HasOne(ci => ci.History)
                .WithOne(ci => ci.Board)
                .HasForeignKey<HistoryEntity>(ci => ci.BoardId)
                .IsRequired(true);
        }
    }
}

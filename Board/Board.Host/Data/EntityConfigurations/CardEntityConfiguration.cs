using Board.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Host.Data.EntityConfigurations
{
    public class CardEntityConfiguration
        : IEntityTypeConfiguration<CardEntity>
    {
        public void Configure(EntityTypeBuilder<CardEntity> builder)
        {
            builder.ToTable("Card");

            builder.Property(ci => ci.CardId)
                .UseHiLo("card_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Description)
                .IsRequired(true)
                .HasMaxLength(500);

            builder.Property(ci => ci.Priority)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.CreatedAt)
                .IsRequired(true);

            builder.HasOne(ci => ci.List)
                .WithMany(ci => ci.Cards)
                .HasForeignKey(ci => ci.ListId)
                .IsRequired(true);
        }
    }
}

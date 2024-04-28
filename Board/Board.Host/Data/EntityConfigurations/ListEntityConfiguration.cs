using Board.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Board.Host.Data.EntityConfigurations
{
    public class ListEntityConfiguration
        : IEntityTypeConfiguration<ListEntity>
    {
        public void Configure(EntityTypeBuilder<ListEntity> builder)
        {
            builder.ToTable("List");

            builder.Property(ci => ci.ListId)
                .UseHiLo("list_hilo")
                .IsRequired();

            builder.Property(ci => ci.StatusName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.HasOne(ci => ci.Board)
                .WithMany(ci => ci.Lists)
                .HasForeignKey(ci => ci.BoardId)
                .IsRequired(true);
        }
    }
}

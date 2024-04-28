using Board.Host.Data.Entities;
using Board.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Board.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<BoardEntity> Boards { get; set; }
        public DbSet<ListEntity> Lists { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<HistoryEntity> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BoardEntityConfiguration());
            builder.ApplyConfiguration(new ListEntityConfiguration());
            builder.ApplyConfiguration(new CardEntityConfiguration());
            builder.ApplyConfiguration(new HistoryEntityConfiguration());
        }
    }
}

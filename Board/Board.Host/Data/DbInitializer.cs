using Board.Host.Data.Entities;

namespace Board.Host.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Boards.Any())
            {
                await context.Boards.AddRangeAsync(GetPreconfiguredBoards());

                await context.SaveChangesAsync();
            }

            if (!context.Lists.Any())
            {
                await context.Lists.AddRangeAsync(GetPreconfiguredLists());

                await context.SaveChangesAsync();
            }

            if (!context.Cards.Any())
            {
                await context.Cards.AddRangeAsync(GetPreconfiguredCards());

                await context.SaveChangesAsync();
            }

            if (!context.Histories.Any())
            {
                await context.Histories.AddRangeAsync(GetPreconfiguredHistories());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<BoardEntity> GetPreconfiguredBoards()
        {
            return new List<BoardEntity>()
        {
            new BoardEntity() { Name = "Test_Board", Description = "Test_Test_Board" }
        };
        }

        private static IEnumerable<ListEntity> GetPreconfiguredLists()
        {
            return new List<ListEntity>()
        {
            new ListEntity() { StatusName = "Canceled", BoardId = 1 },
            new ListEntity() { StatusName = "Passed", BoardId = 1 }
        };
        }

        private static IEnumerable<CardEntity> GetPreconfiguredCards()
        {
            return new List<CardEntity>()
        {
            new CardEntity() { Name = "Test_name1", Description = "Test_Test_name1", Priority = "High", CreatedAt = DateTime.UtcNow, ListId = 1 },
            new CardEntity() { Name = "Test_name2", Description = "Test_Test_name2", Priority = "Medium", CreatedAt = DateTime.UtcNow, ListId = 1 },
            new CardEntity() { Name = "Test_name3", Description = "Test_Test_name3", Priority = "High", CreatedAt = DateTime.UtcNow, ListId = 1 },
            new CardEntity() { Name = "Test_name4", Description = "Test_Test_name4", Priority = "WithoutPriority", CreatedAt = DateTime.UtcNow, ListId = 2 }
        };
        }

        private static IEnumerable<HistoryEntity> GetPreconfiguredHistories()
        {
            return new List<HistoryEntity>()
        {
            new HistoryEntity() { Type = "Test_type1", Date = DateTime.UtcNow, BoardId = 1 }
        };
        }
    }
}

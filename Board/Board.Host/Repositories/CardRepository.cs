using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;

namespace Board.Host.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CardRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }
        public async Task<int> CreateCardAsync(string name, string description, string priority, int listId)
        {
            var card = await _dbContext.AddAsync(new CardEntity
            {
                Name = name,
                Description = description,
                Priority = priority,
                CreatedAt = DateTime.UtcNow,
                ListId = listId
            });

            await _dbContext.SaveChangesAsync();

            return card.Entity.CardId;
        }

        public async Task<bool> DeleteCardAsync(int id)
        {
            try
            {
                var card = _dbContext.Cards.FirstOrDefault(d => d.CardId == id);

                if (card == null)
                {
                    throw new ArgumentNullException($"Not found card with id:{id}");
                }

                _dbContext.Cards.Remove(card);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }

        public async Task<CardDto> GetCardAsync(int id)
        {
            try
            {
                var card = _dbContext.Cards.FirstOrDefault(d => d.CardId == id);

                if (card == null)
                {
                    throw new ArgumentNullException($"Not found card with id:{id}");
                }

                return new CardDto() { CardId = card.CardId, Name = card.Name, Description = card.Description, Priority = card.Priority, CreatedAt = card.CreatedAt, ListId = card.ListId };
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return null;
            }
        }

        public async Task<bool> UpdateCardAsync(int id, string name, string description, string priority)
        {
            try
            {
                var card = _dbContext.Cards.FirstOrDefault(d => d.CardId == id);

                if (card == null)
                {
                    throw new ArgumentNullException($"Not found card with id:{id}");
                }

                if (!string.IsNullOrEmpty(name) && name != "string")
                {
                    card.Name = name;
                }

                if (!string.IsNullOrEmpty(description) && description != "string")
                {
                    card.Description = description;
                }

                if (!string.IsNullOrEmpty(priority) && priority != "string")
                {
                    card.Priority = priority;
                }

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }
    }
}

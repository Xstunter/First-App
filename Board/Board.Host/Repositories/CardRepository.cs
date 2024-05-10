using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Board.Host.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CardRepository> _logger;
        private readonly IHistoryService _historyService;

        public CardRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CardRepository> logger,
            IHistoryService historyService)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
            _historyService = historyService;
        }

        public async Task<bool> ChangeListAsync(int cardId, int listId, int boardId)
        {
            try
            {
                var list = await _dbContext.Lists.FindAsync(listId);

                if (list == null)
                {
                    throw new KeyNotFoundException($"List with ID {listId} not found.");
                }

                var card = await _dbContext.Cards.FindAsync(cardId);

                if (card == null)
                {
                    throw new KeyNotFoundException($"Card with ID {cardId} not found.");
                }
                
                card.ListId = listId;

                await _dbContext.SaveChangesAsync();

                await _historyService.AddHistory($"You moved card {card.Name} to {list.StatusName}", boardId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }

        public async Task<int> CreateCardAsync(string name, string description, string priority, int listId, int boardId)
        {
            var card = await _dbContext.AddAsync(new CardEntity
            {
                Name = name,
                Description = description,
                Priority = priority,
                CreatedAt = DateTime.UtcNow,
                ListId = listId
            });

            await _historyService.AddHistory($"You added card {name}", boardId);

            await _dbContext.SaveChangesAsync();

            return card.Entity.CardId;
        }

        public async Task<bool> DeleteCardAsync(int id, int boardId)
        {
            try
            {
                var card = _dbContext.Cards.FirstOrDefault(d => d.CardId == id);

                if (card == null)
                {
                    throw new ArgumentNullException($"Not found card with id:{id}");
                }

                await _historyService.AddHistory($"You delete card {card.Name}", boardId);

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

        public async Task<IEnumerable<CardDto>> GetAllListsCardAsync(int listId)
        {
            try
            {
                var list = await _dbContext.Lists.FindAsync(listId);

                if (list == null)
                {
                    throw new KeyNotFoundException($"List with ID {listId} not found.");
                }

                var cards = await _dbContext.Cards
                    .Where(d => d.ListId == listId)
                    .Select(card => new CardDto()
                    {
                        CardId = card.CardId,
                        Name = card.Name,
                        Description = card.Description,
                        Priority = card.Priority,
                        ListId = card.ListId,
                        CreatedAt = card.CreatedAt
                    })
                    .ToListAsync();

                return cards;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return null;
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

        public async Task<bool> UpdateCardAsync(int id, string name, string description, string priority, int boardId)
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
                    if (card.Name != name)
                    {
                        await _historyService.AddHistory($"You renamed card {card.Name} to {name}", boardId);
                    }
                    card.Name = name;
                }

                if (!string.IsNullOrEmpty(description) && description != "string")
                {
                    if (card.Description != description)
                    {
                        await _historyService.AddHistory($"You redescription card {card.Description} to {description}", boardId);
                    }
                    card.Description = description;
                }

                if (!string.IsNullOrEmpty(priority) && priority != "string")
                {
                    if (card.Priority != priority)
                    {
                        await _historyService.AddHistory($"You change the priotity {name} from {card.Priority} to {priority}", boardId);
                    }
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

using Board.Host.Data;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;

namespace Board.Host.Services
{
    public class CardService : BaseDataService<ApplicationDbContext>, ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICardRepository cardRepository)
            : base(dbContextWrapper, logger)
        {
            _cardRepository = cardRepository;
        }

        public Task<int> CreateCardAsync(string name, string description, string priority, int listId)
        {
            return ExecuteSafeAsync(() => _cardRepository.CreateCardAsync(name, description, priority, listId));
        }

        public Task<bool> DeleteCardAsync(int id)
        {
            return ExecuteSafeAsync(() => _cardRepository.DeleteCardAsync(id));
        }

        public Task<CardDto> GetCardAsync(int id)
        {
            return ExecuteSafeAsync(() => _cardRepository.GetCardAsync(id));
        }

        public Task<bool> UpdateCardAsync(int id, string name, string description, string priority)
        {
            return ExecuteSafeAsync(() => _cardRepository.UpdateCardAsync(id, name, description, priority));
        }
    }
}

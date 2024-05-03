using Board.Host.Models.Dtos;

namespace Board.Host.Repositories.Interfaces
{
    public interface ICardRepository
    {
        public Task<int> CreateCardAsync(string name, string description, string priority, int listId);
        public Task<bool> UpdateCardAsync(int id, string name, string description, string priority);
        public Task<bool> ChangeListAsync(int cardId, int listId);
        public Task<bool> DeleteCardAsync(int id);
        public Task<CardDto> GetCardAsync(int id);
        public Task<IEnumerable<CardDto>> GetAllListsCardAsync(int listId);
    }
}

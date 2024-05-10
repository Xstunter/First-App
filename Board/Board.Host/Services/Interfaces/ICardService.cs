using Board.Host.Models.Dtos;

namespace Board.Host.Services.Interfaces
{
    public interface ICardService
    {
        public Task<int> CreateCardAsync(string name, string description, string priority, int listId, int boardId);
        public Task<bool> UpdateCardAsync(int id, string name, string description, string priority, int boardId);
        public Task<bool> ChangeListAsync(int cardId, int listId, int boardId);
        public Task<bool> DeleteCardAsync(int id, int boardId);
        public Task<CardDto> GetCardAsync(int id);
        public Task<IEnumerable<CardDto>> GetAllListsCardAsync(int listId);
    }
}

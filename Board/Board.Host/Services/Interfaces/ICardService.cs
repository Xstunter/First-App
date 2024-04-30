using Board.Host.Models.Dtos;

namespace Board.Host.Services.Interfaces
{
    public interface ICardService
    {
        public Task<int> CreateCardAsync(string name, string description, string priority, int listId);
        public Task<bool> UpdateCardAsync(int id, string name, string description, string priority);
        public Task<bool> DeleteCardAsync(int id);
        public Task<CardDto> GetCardAsync(int id);
    }
}

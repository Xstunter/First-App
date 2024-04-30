using Board.Host.Models.Dtos;

namespace Board.Host.Services.Interfaces
{
    public interface IListService
    {
        public Task<int> CreateListAsync(string name, int boardId);
        public Task<bool> UpdateListAsync(int id, string name);
        public Task<bool> DeleteListAsync(int id);
        public Task<ListDto> GetListAsync(int id);
    }
}

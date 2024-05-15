using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;

namespace Board.Host.Repositories.Interfaces
{
    public interface IListRepository
    {
        public Task<int?> CreateListAsync(string name, int boardId);
        public Task<bool> UpdateListAsync(int id, string name);
        public Task<bool> DeleteListAsync(int id);
        public Task<ListDto> GetListAsync(int id);
        public Task<IEnumerable<ListDto>> GetAllListAsync(int boardId);
    }
}

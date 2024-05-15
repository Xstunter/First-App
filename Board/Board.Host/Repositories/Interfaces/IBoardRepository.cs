using Board.Host.Models.Dtos;

namespace Board.Host.Repositories.Interfaces
{
    public interface IBoardRepository
    {
        public Task<int> CreateBoardAsync(string name, string description);
        public Task<bool> UpdateBoardAsync(int id, string name, string description);
        public Task<bool> DeleteBoardAsync(int id);
        public Task<BoardDto> GetBoardAsync(int id);
        public Task<IEnumerable<BoardDto>> GetAllBoardAsync();
    }
}

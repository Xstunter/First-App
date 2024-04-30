using Board.Host.Models.Dtos;

namespace Board.Host.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<int> CreateBoardAsync(string name, string description);
        public Task<bool> UpdateBoardAsync(int id, string name, string description);
        public Task<bool> DeleteBoardAsync(int id);
        public Task<BoardDto> GetBoardAsync(int id);
    }
}

using Board.Host.Models.Dtos;

namespace Board.Host.Repositories.Interfaces
{
    public interface IHistoryRepository
    {
        public Task AddHistory(string type, int boardId);
        public Task<IEnumerable<HistoryDto>> GetBoardHistory(int boardId);
    }
}

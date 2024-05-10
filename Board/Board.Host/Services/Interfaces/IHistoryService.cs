using Board.Host.Models.Dtos;

namespace Board.Host.Services.Interfaces
{
    public interface IHistoryService 
    {
        public Task AddHistory(string type, int boardId);
        public Task<IEnumerable<HistoryDto>> GetBoardHistory(int boardId);
    }
}

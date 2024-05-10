using Board.Host.Data;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;

namespace Board.Host.Services
{
    public class HistoryService : BaseDataService<ApplicationDbContext>, IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IHistoryRepository historyRepository)
            : base(dbContextWrapper, logger)
        {
            _historyRepository = historyRepository;
        }
        public async Task AddHistory(string type, int boardId)
        {
            await _historyRepository.AddHistory(type, boardId);
        }

        public Task<IEnumerable<HistoryDto>> GetBoardHistory(int boardId)
        {
            return ExecuteSafeAsync(() => _historyRepository.GetBoardHistory(boardId));
        }
    }
}

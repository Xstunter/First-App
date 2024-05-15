using Board.Host.Data;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;

namespace Board.Host.Services
{
    public class BoardService : BaseDataService<ApplicationDbContext>, IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBoardRepository boardRepository)
            : base(dbContextWrapper, logger)
        {
            _boardRepository = boardRepository;
        }

        public Task<int> CreateBoardAsync(string name, string description)
        {
            return ExecuteSafeAsync(() => _boardRepository.CreateBoardAsync(name, description));
        }

        public Task<bool> DeleteBoardAsync(int id)
        {
            return ExecuteSafeAsync(() => _boardRepository.DeleteBoardAsync(id));
        }

        public Task<IEnumerable<BoardDto>> GetAllBoardAsync()
        {
            return ExecuteSafeAsync(() => _boardRepository.GetAllBoardAsync());
        }

        public Task<BoardDto> GetBoardAsync(int id)
        {
            return ExecuteSafeAsync(() => _boardRepository.GetBoardAsync(id));
        }

        public Task<bool> UpdateBoardAsync(int id, string name, string description)
        {
           return ExecuteSafeAsync(() => _boardRepository.UpdateBoardAsync(id, name, description));
        }
    }
}

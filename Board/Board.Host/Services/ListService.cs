using Board.Host.Data;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;

namespace Board.Host.Services
{
    public class ListService : BaseDataService<ApplicationDbContext>, IListService
    {
        private readonly IListRepository _listRepository;
        public ListService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IListRepository listRepository)
            : base(dbContextWrapper, logger)
        {
            _listRepository = listRepository;
        }
        public Task<int> CreateListAsync(string name, int boardId)
        {
            return ExecuteSafeAsync(() => _listRepository.CreateListAsync(name, boardId));
        }

        public Task<bool> DeleteListAsync(int id)
        {
            return ExecuteSafeAsync(() => _listRepository.DeleteListAsync(id));
        }

        public Task<IEnumerable<ListDto>> GetAllListAsync(int boardId)
        {
            return ExecuteSafeAsync(() => _listRepository.GetAllListAsync(boardId));
        }

        public Task<ListDto> GetListAsync(int id)
        {
            return ExecuteSafeAsync(() => _listRepository.GetListAsync(id));
        }

        public Task<bool> UpdateListAsync(int id, string name)
        {
            return ExecuteSafeAsync(() => _listRepository.UpdateListAsync(id, name));
        }
    }
}

using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Host.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ListRepository> _logger;

        public ListRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<ListRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }
        public async Task<int> CreateListAsync(string name, int boardId)
        {
            var list = await _dbContext.AddAsync(new ListEntity
            {
                StatusName = name,
                BoardId = boardId
            });

            await _dbContext.SaveChangesAsync();

            return list.Entity.ListId;
        }

        public async Task<bool> DeleteListAsync(int id)
        {
            try
            {
                var list = _dbContext.Lists.FirstOrDefault(d => d.ListId == id);

                if (list == null)
                {
                    throw new ArgumentNullException($"Not found list with id:{id}");
                }

                _dbContext.Lists.Remove(list);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }

        public async Task<ListDto> GetListAsync(int id)
        {
            try
            {
                var list = _dbContext.Lists.FirstOrDefault(d => d.ListId == id);

                if (list == null)
                {
                    throw new ArgumentNullException($"Not found list with id:{id}");
                }

                return new ListDto() { ListId = list.ListId, StatusName = list.StatusName, BoardId = list.BoardId};
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return null;
            }
        }

        public async Task<bool> UpdateListAsync(int id, string name)
        {
            try
            {
                var list = _dbContext.Lists.FirstOrDefault(d => d.ListId == id);

                if (list == null)
                {
                    throw new ArgumentNullException($"Not found list with id:{id}");
                }

                if (!string.IsNullOrEmpty(name) && name != "string")
                {
                    list.StatusName = name;
                }

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }
    }
}

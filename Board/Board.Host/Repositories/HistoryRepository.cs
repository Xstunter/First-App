using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Host.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HistoryRepository> _logger;

        public HistoryRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<HistoryRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task  AddHistory(string type, int boardId)
        {
            try
            {
                var history = await _dbContext.AddAsync(new HistoryEntity
                {
                    Type = type,
                    Date = DateTime.UtcNow,
                    BoardId = boardId
                });

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
            }
        }

        public async Task<IEnumerable<HistoryDto>> GetBoardHistory(int boardId)
        {
            try
            {
                var histories = await _dbContext.Histories
                    .Where(history => history.BoardId == boardId)
                    .Select(history => new HistoryDto
                    {
                        HistoryId = history.HistoryId,
                        Type = history.Type,
                        Date = history.Date,
                        BoardId = history.BoardId
                    })
                    .ToListAsync();

                return histories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting histories for board with ID {boardId}");
                throw;
            }
        }
    }
}
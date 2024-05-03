using Board.Host.Data;
using Board.Host.Data.Entities;
using Board.Host.Models.Dtos;
using Board.Host.Repositories.Interfaces;
using Board.Host.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Board.Host.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BoardRepository> _logger;

        public BoardRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BoardRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<int> CreateBoardAsync(string name, string description)
        {
            var board = await _dbContext.AddAsync(new BoardEntity 
            { 
                Name = name, 
                Description = description 
            });

            await _dbContext.SaveChangesAsync();

            return board.Entity.BoardId;
        }

        public async Task<bool> DeleteBoardAsync(int id)
        {
            try
            {
                var board = _dbContext.Boards.FirstOrDefault(d => d.BoardId == id);

                if (board == null)
                {
                    throw new ArgumentNullException($"Not found board with id:{id}");
                }

                _dbContext.Boards.Remove(board);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogInformation(ex.Message);

                return false;
            }
        }

        public async Task<BoardDto> GetBoardAsync(int id)
        {
            try
            {
                var board = _dbContext.Boards.FirstOrDefault(d => d.BoardId == id);

                if (board == null)
                {
                    throw new ArgumentNullException($"Not found board with id:{id}");
                }

                return new BoardDto() { BoardId = board.BoardId, Name = board.Name, Description = board.Description};
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return null;
            }
        }

        public async Task<bool> UpdateBoardAsync(int id, string name, string description)
        {
            try
            {
                var board = _dbContext.Boards.FirstOrDefault(d => d.BoardId == id);

                if (board == null)
                {
                    throw new ArgumentNullException($"Not found board with id:{id}");
                }

                if (!string.IsNullOrEmpty(name) && name != "string")
                {
                    board.Name = name;
                }

                if (!string.IsNullOrEmpty(description) && description != "string")
                {
                    board.Description = description;
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

using Board.Host.Models.Dtos;
using Board.Host.Models.Requests;
using Board.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IBoardService _boardService;

        public BoardController(
            ILogger<BoardController> logger,
            IBoardService boardService)
        {
            _logger = logger;
            _boardService = boardService;
        }

        [HttpPost("/CreateBoard")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> CreateBoard(CreateBoardRequest request)
        {
            var result = await _boardService.CreateBoardAsync(request.Name, request.Description);
            return Ok(result);
        }
        [HttpDelete("/DeleteBoard")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> DeleteBoard(DeleteBoardRequest request)
        {
            var isDeleted = await _boardService.DeleteBoardAsync(request.BoardId);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("/UpdatedBoard")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> UpdateBoard(UpdateBoardRequest request)
        {
            var isUpdated = await _boardService.UpdateBoardAsync(request.BoardId, request.Name, request.Description);
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/GetBoard")]
        [ProducesResponseType(typeof(BoardDto), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetBoard(int boardId)
        {
            var isGet = await _boardService.GetBoardAsync(boardId);
            if (isGet != null)
            {
                return Ok(isGet);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

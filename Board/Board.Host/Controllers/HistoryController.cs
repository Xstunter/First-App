using Board.Host.Models.Dtos;
using Board.Host.Models.Requests;
using Board.Host.Services;
using Board.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly IHistoryService _historyService;

        public HistoryController(
            ILogger<HistoryController> logger,
            IHistoryService historyService)
        {
            _historyService = historyService;
            _logger = logger;
        }

        [HttpGet("/GetBoardHistory")]
        [ProducesResponseType(typeof(IEnumerable<HistoryDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetBoardHistory(int boardId)
        {
            var isGet = await _historyService.GetBoardHistory(boardId);
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

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
    public class ListController : ControllerBase
    {
        private readonly ILogger<ListController> _logger;
        private readonly IListService _listService;

        public ListController(
            ILogger<ListController> logger,
            IListService listService)
        {
            _listService = listService;
            _logger = logger;
        }

        [HttpPost("/CreateList")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> CreateList(CreateListRequest request)
        {
            var result = await _listService.CreateListAsync(request.StatusName, request.BoardId);
            if (result != 0)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("/DeleteList")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> DeleteList(DeleteListRequest request)
        {
            var isDeleted = await _listService.DeleteListAsync(request.ListId);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("/UpdatedList")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> UpdateList(UpdateListRequest request)
        {
            var isUpdated = await _listService.UpdateListAsync(request.ListId, request.StatusName);
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/GetList")]
        [ProducesResponseType(typeof(ListDto), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetList(int listId)
        {
            var isGet = await _listService.GetListAsync(listId);
            if (isGet != null)
            {
                return Ok(isGet);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/GetAllLists")]
        [ProducesResponseType(typeof(IEnumerable<ListDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAllLists(int boardId)
        {
            var isGet = await _listService.GetAllListAsync(boardId);
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

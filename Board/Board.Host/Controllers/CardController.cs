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
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _cardService;

        public CardController(
            ILogger<CardController> logger,
            ICardService cardService)
        {
            _cardService = cardService;
            _logger = logger;
        }

        [HttpPost("/CreateCard")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> CreateCard(CreateCardRequest request)
        {
            var result = await _cardService.CreateCardAsync(request.Name, request.Description, request.Priority, request.ListId, request.BoardId);
            return Ok(result);
        }
        [HttpDelete("/DeleteCard")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> DeleteCard(DeleteCardRequest request)
        {
            var isDeleted = await _cardService.DeleteCardAsync(request.CardId, request.BoardId);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("/UpdatedCard")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> UpdateCard(UpdateCardRequest request)
        {
            var isUpdated = await _cardService.UpdateCardAsync(request.CardId, request.Name, request.Description, request.Priority, request.BoardId);
            if (isUpdated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPatch("/ChangeListForCard")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> ChangeListForCard(ChangeListForCardRequest request)
        {
            var isChanged = await _cardService.ChangeListAsync(request.CardId, request.ListId, request.BoardId);
            if (isChanged)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/GetCard")]
        [ProducesResponseType(typeof(CardDto), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetCard(int cardId)
        {
            var isGet = await _cardService.GetCardAsync(cardId);
            if (isGet != null)
            {
                return Ok(isGet);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("/GetAllListsCard")]
        [ProducesResponseType(typeof(IEnumerable<CardDto>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetAllListsCard(int listId)
        {
            var isGet = await _cardService.GetAllListsCardAsync(listId);
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

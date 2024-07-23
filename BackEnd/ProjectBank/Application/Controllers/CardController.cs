using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            var card = await _cardService.Get(search, sortItem, sortOrder);

            return Ok(card);
        }


        [HttpPost]
        public async Task<ActionResult<Card>> Post(CardRequestModel card)
        {

            var createdCard = await _cardService.Post(card);
            return Ok(createdCard);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CardRequestModel card)
        {
            await _cardService.Update(id, card);
            return Ok(id);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _cardService.Delete(id);
            return NoContent();
        }
    }
}

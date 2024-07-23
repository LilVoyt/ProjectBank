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
        private readonly DataContext _context;
        private readonly ICardService cardService;

        public CardController(DataContext context, ICardService cardService)
        {
            _context = context;
            this.cardService = cardService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Card>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            var card = await cardService.Get(search, sortItem, sortOrder);

            return Ok(card);
        }


        [HttpPost]
        public async Task<ActionResult<Card>> Post(CardRequestModel card)
        {

            var createdCard = await cardService.Post(card);
            return Ok(createdCard);
        }


        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CardRequestModel card)
        {
            await cardService.Update(id, card);
            return Ok(id);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await cardService.Delete(id);
            return NoContent();
        }
    }
}

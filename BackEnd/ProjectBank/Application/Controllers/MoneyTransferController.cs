using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/MakeTransactions")]
    [ApiController]
    public class MoneyTransferController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMoneyTransferService methodsService;

        public MoneyTransferController(DataContext context, IMoneyTransferService methodsSevice)
        {
            _context = context;
            methodsService = methodsSevice;
        }
        [HttpGet("MakeTransaction/cardID={cardID}/sum={sum}")]
        public async Task<ActionResult<Account>> MakeTransaction(Guid cardID, double sum)
        {
            await methodsService.MakeTransaction(cardID, sum);
            return Ok();
        }

        [HttpPut("makeTransaction/senderCardID={senderCardID}/receiverCardID={receiverCardID}/sum={sum}")]
        public async Task<ActionResult<Account>> MakeTransaction(Guid senderCardID, Guid receiverCardID, double sum)
        {
            await methodsService.MakeTransaction(senderCardID, receiverCardID, sum);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/makeTransactions")]
    [ApiController]
    public class MoneyTransferController : ControllerBase
    {
        private readonly IMoneyTransferService _moneyTransferService;

        public MoneyTransferController(IMoneyTransferService methodsSevice)
        {
            _moneyTransferService = methodsSevice;
        }
        [HttpGet]
        public async Task<ActionResult<Account>> MakeTransaction(Guid cardID, double sum)
        {
            await _moneyTransferService.MakeTransaction(cardID, sum);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Account>> MakeTransaction(Guid senderCardID, Guid receiverCardID, double sum)
        {
            await _moneyTransferService.MakeTransaction(senderCardID, receiverCardID, sum);
            return Ok();
        }
    }
}

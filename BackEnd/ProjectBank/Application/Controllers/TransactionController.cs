using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Transactions;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Entities.Transaction>>> Get(Guid? search, string? sortItem, string? sortOrder)
        {
            var transaction = await transactionService.Get(search, sortItem, sortOrder);

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Entities.Transaction>> Post(TransactionRequestModel transaction)
        {

            var createdTransaction = await transactionService.Post(transaction);
            return Ok(createdTransaction);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, TransactionRequestModel transaction)
        {
            await transactionService.Update(id, transaction);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await transactionService.Delete(id);

            return NoContent();
        }
    }
}

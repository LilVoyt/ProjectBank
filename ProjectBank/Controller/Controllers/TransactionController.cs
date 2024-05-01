using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
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
        private readonly DataContext _context;
        private readonly ITransactionService transactionService;

        public TransactionController(DataContext context, ITransactionService transactionService)
        {
            _context = context;
            this.transactionService = transactionService;
        }

        [HttpGet("GetAllTransaction")]
        public async Task<ActionResult<List<Transaction>>> GetAllTransaction() //work
        {
            var transaction = await transactionService.GetAllTransaction();

            return Ok(transaction);
        }

        [HttpGet("GetTransaction/{id}")]
        public async Task<ActionResult<TransactionRequestModel>> GetTransaction(Guid id) //work
        {

            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var transaction = await transactionService.GetTransactions(id);

            return Ok(transaction);
        }

        [HttpPost("AddTransaction")]
        public async Task<ActionResult<Transaction>> AddTransaction(TransactionRequestModel transaction)
        {
            try
            {
                var createdTransaction = await transactionService.AddTransactions(transaction);
                return Ok(createdTransaction);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteTransaction/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await transactionService.DeleteTransactions(id);

            return NoContent();
        }

        [HttpPut("UpdateTransaction/{id}")]
        public async Task<IActionResult> UpdateTransactions(Guid id, TransactionRequestModel transaction) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await transactionService.UpdateTransactions(id, transaction);
            return Ok(id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IAccountService accountService;

        public AccountController(DataContext context, IAccountService accountService)
        {
            _context = context;
            this.accountService = accountService;
        }

        [HttpGet("GetAllAccount")]
        public async Task<ActionResult<List<Account>>> GetAllAccount() //work
        {
            var account = await accountService.GetAllAccount();

            return Ok(account);
        }

        [HttpGet("GetAccount/{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id) //work
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("AddAccount")]
        public async Task<ActionResult<Account>> AddAccount([FromBody] AccountRequestModel account)
        {
            try
            {
                var createdAccount = await accountService.AddAccount(account);
                return Ok(createdAccount);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);  
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateAccount/{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, AccountRequestModel account) //Work
        {
            if (id == Guid.Empty)                       
            {
                return BadRequest();
            }
            await accountService.UpdateAccount(id, account);
            return Ok(id);
        }

        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await accountService.DeleteAccount(id);
            return NoContent();
        }

    }
}

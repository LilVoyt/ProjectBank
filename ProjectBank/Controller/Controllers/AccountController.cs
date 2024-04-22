using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;

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

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccount() //work
        {
            var account = await accountService.GetAllAccount();

            return Ok(account);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id) //work
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddAccount(Account account) //тут
        {
            await accountService.AddAccount(account);

            return Ok(await GetAllAccount());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, Account customer) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await accountService.UpdateAccount(id, customer);
            return Ok(id);
        }

        [HttpDelete("{id}")]
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

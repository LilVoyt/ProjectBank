using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Entities;
using ProjectBank.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts() // Get all accounts
        {
            var accounts = await _accountService.GetAllAccount();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(Guid id) // Get account by ID
        {
            var account = await _accountService.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(AccountRequestModel account)
        {
            try
            {
                var createdAccount = await _accountService.AddAccount(account);
                return CreatedAtAction(nameof(GetAccount), new { id = createdAccount.Id }, createdAccount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, AccountRequestModel account) // Update account by ID
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await _accountService.UpdateAccount(id, account);
            if (result == Guid.Empty)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id) // Delete account by ID
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await _accountService.DeleteAccount(id);
            if (result == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Services.Interfaces;
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
        public async Task<ActionResult<List<AccountRequestModel>>> Get(string? Search, string? SortItem, string? SortOrder)
        {
            var accounts = await _accountService.Get(Search, SortItem, SortOrder);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(AccountRequestModel account)
        {
            var createdAccount = await _accountService.Post(account);
            return CreatedAtAction(nameof(AddAccount), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, AccountRequestModel account)
        {
            var result = await _accountService.Update(id, account);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            await _accountService.Delete(id);
            return NoContent();
        }
    }
}

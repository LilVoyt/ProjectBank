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
        public async Task<ActionResult<Account>> Post(AccountRequestModel account)
        {
            var createdAccount = await _accountService.Post(account);
            return CreatedAtAction(nameof(Post), new { id = createdAccount.Id }, createdAccount);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, AccountRequestModel account)
        {
            var result = await _accountService.Update(id, account);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _accountService.Delete(id);
            return NoContent();
        }
    }
}

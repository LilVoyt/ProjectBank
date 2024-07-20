using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Entities;
using ProjectBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Data.SqlClient;

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
        public async Task<ActionResult<List<Account>>> Get(string? Search, string? SortItem, string? SortOrder) // Get all accounts
        {
            var accounts = await _accountService.Get(Search, SortItem, SortOrder);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<ActionResult<Account>> AddAccount(AccountRequestModel account) //from body
        {
            var createdAccount = await _accountService.Post(account);
            return Ok(createdAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, AccountRequestModel account) // Update account by ID
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            var result = await _accountService.Update(id, account);
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
            var result = await _accountService.Delete(id);
            if (result == Guid.Empty)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

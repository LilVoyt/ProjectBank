using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Linq.Expressions;
using System.Net;
using System.Threading;

namespace ProjectBank.Controller.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IValidator<Account> _validator;
        private readonly AccountMapper _accountMapper;

        public AccountService(DataContext context, IValidator<Account> validator, AccountMapper accountMapper)
        {
            _context = context;
            _validator = validator;
            _accountMapper = accountMapper;
        }

        public async Task<ActionResult<List<AccountRequestModel>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Account> accounts = _context.Account;

            if (!string.IsNullOrEmpty(search))
            {
                accounts = accounts.Where(n => n.Name.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Account, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => account => account.Name,
                _ => account => account.Id,
            };

            accounts = sortOrder?.ToLower() == "desc"
                ? accounts.OrderByDescending(selectorKey)
                : accounts.OrderBy(selectorKey);

            List<Account> accountList = await accounts.ToListAsync();

            List<AccountRequestModel> response = _accountMapper.GetRequestModels(accountList);

            return response;
        }

        public async Task<Account> Post(AccountRequestModel account)
        {
            var res = _accountMapper.GetAccount(account);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _context.Account.AddAsync(res);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Account> Update(Guid id, AccountRequestModel requestModel)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            account = _accountMapper.PutAccountRequestModelInAccount(account, requestModel);
            var validationResult = await _validator.ValidateAsync(account);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            _context.Account.Update(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> Delete(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            account.EmployeeID = Guid.Empty;
            account.CustomerID = Guid.Empty;
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}

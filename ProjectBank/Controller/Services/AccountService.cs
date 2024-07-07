using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services.Mappers;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Net;
using System.Threading;

namespace ProjectBank.Controller.Services
{
    public interface IAccountService
    {
        Task<ActionResult<List<Account>>> GetAllAccount();
        Task<AccountRequestModel> GetAccount(Guid id);
        Task<Account> AddAccount(AccountRequestModel account);
        Task<Guid> UpdateAccount(Guid id, AccountRequestModel account);
        Task<Guid> DeleteAccount(Guid id);
    }

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

        public async Task<ActionResult<List<Account>>> GetAllAccount()
        {
            var account = await _context.Account.ToListAsync();
            return account;
        }

        public async Task<AccountRequestModel> GetAccount(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return null;
            }

            var res = _accountMapper.MapRequestToDB(account);
            return res;
        }

        public async Task<Account> AddAccount(AccountRequestModel account)
        {
            var res = _accountMapper.MapRequestToAccount(account);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errorMessages);
            }

            await _context.Account.AddAsync(res);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Guid> DeleteAccount(Guid id)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            account.EmployeeID = Guid.Empty;
            account.CustomerID = Guid.Empty;
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Guid> UpdateAccount(Guid id, AccountRequestModel requestModel)
        {
            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            account = _accountMapper.MapRequestToSet(account, requestModel);
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}

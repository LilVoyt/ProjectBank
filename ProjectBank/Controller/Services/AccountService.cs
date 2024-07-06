using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AccountService(DataContext context, IValidator<Account> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ActionResult<List<Account>>> GetAllAccount()
        {
            var account = await _context.Accounts.ToListAsync();

            return account;
        }

        public async Task<AccountRequestModel> GetAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                return null;
            }

            var res = MapRequestToDB(account);

            return res;
        }

        public async Task<Account> AddAccount(AccountRequestModel account)
        {
            var res = MapRequestToAccount(account);

            var validationResult = await _validator.ValidateAsync(res);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errorMessages);
            }

            await _context.Accounts.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }


        public async Task<Guid> DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            account.EmployeeID = Guid.Empty;
            account.CustomerID = Guid.Empty;
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }


        public async Task<Guid> UpdateAccount(Guid id, AccountRequestModel requestModel)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }
            account = MapRequestToSet(account, requestModel);
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();

            return id;
        }


        private Account MapRequestToAccount(AccountRequestModel requestModel)
        {
            var account = new Account();
            account.Id = Guid.NewGuid();
            account.Name = requestModel.Name;
            account.EmployeeID = requestModel.EmployeeID;
            account.CustomerID = requestModel.CustomerID;

            return account;
        }

        private AccountRequestModel MapRequestToDB(Account account)
        {
            var requestModel = new AccountRequestModel();
            requestModel.Name = account.Name;
            requestModel.EmployeeID = account.EmployeeID;
            requestModel.CustomerID = account.CustomerID;

            return requestModel;
        }

        private Account MapRequestToSet(Account res, AccountRequestModel account)
        {
            res.Name = account.Name;
            res.EmployeeID = account.EmployeeID;
            res.CustomerID = account.CustomerID;

            return res;
        }

    }
}

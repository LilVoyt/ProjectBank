using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public interface IAccountService
    {
        Task<ActionResult<List<Account>>> GetAllAccount();
        Task<Customer> GetAccount(Guid id);
        Task<Guid> AddAccount(Customer customer);
        Task<Guid> UpdateAccount(Guid id);
        Task<Guid> DeleteAccount(Guid id);
    }

    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAccount(Account account)
        {
            account.Id = Guid.NewGuid();
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return account.Id;
        }


        public async Task<Guid> DeleteAccount(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<ActionResult<List<Account>>> GetAllCustomers()
        {
            var customers = await _context.Accounts.ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public async Task<Guid> UpdateCustomer(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

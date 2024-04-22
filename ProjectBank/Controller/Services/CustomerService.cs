using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public interface ICustomerService
    {
        Task<ActionResult<List<Customer>>> GetAllCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task<Guid> AddCustomer(Customer customer);
        Task<Guid> UpdateCustomer(Guid id, Customer newChanges);
        Task<Guid> DeleteCustomer(Guid id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }


        public async Task<Guid> DeleteCustomer(Guid id)
        {
            var account = await _context.Customers.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            _context.Customers.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

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

        public async Task<Guid> UpdateCustomer(Guid id, Customer newChanges)//need changes
        {
            var account = await _context.Customers.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }
            account.Name = newChanges.Name;
            account.LastName = newChanges.LastName;
            account.Country = newChanges.Country;
            account.Phone = newChanges.Phone;
            account.Email = newChanges.Email;
            _context.Customers.Update(account);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

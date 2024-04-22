using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjectBank.Data;
using ProjectBank.Entities;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task<ActionResult<List<Customer>>> AddCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(Guid id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return await GetAllCustomers();
        }


        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private IActionResult NoContent()
        {
            return EmptyHttpResult();
        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException("Error with not found");
        }

        public Task<List<Customer>> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        Task ICustomerService.DeleteCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        // Implement methods for GetAllCustomers, GetCustomer, AddCustomer, UpdateCustomer, DeleteCustomer
    }
}

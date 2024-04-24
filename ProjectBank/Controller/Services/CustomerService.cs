using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public interface ICustomerService
    {
        Task<ActionResult<List<Customer>>> GetAllCustomers();
        Task<CustomerRequestModel> GetCustomer(Guid id);
        Task<Customer> AddCustomer(CustomerRequestModel customer);
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
        public async Task<Customer> AddCustomer(CustomerRequestModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var res = MapRequestToCustomer(customer);

            _context.Customers.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
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

        public async Task<CustomerRequestModel> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            var res = MapRequestToDB(customer);

            if (customer == null)
            {
                return null;
            }

            return res;
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
        private Customer MapRequestToCustomer(CustomerRequestModel requestModel)
        {
            var customer = new Customer();
            customer.Id = Guid.NewGuid();
            customer.Name = requestModel.Name;
            customer.LastName = requestModel.LastName;
            customer.Country = requestModel.Country;
            customer.Phone = requestModel.Phone;
            customer.Email = requestModel.Email;
            return customer;
        }

        private CustomerRequestModel MapRequestToDB(Customer customer)
        {
            var requestModel = new CustomerRequestModel();
            requestModel.Name = customer.Name;
            requestModel.LastName = customer.LastName;
            requestModel.Country = customer.Country;
            requestModel.Phone = customer.Phone;
            requestModel.Email = customer.Email;

            return requestModel;
        }
    }
}

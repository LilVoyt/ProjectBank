using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Application.Validators.Customer;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;
        private readonly CustomerMapper _customerMapper;

        public CustomerService(DataContext context, CustomerMapper customerMapper, IValidator<Account> validator)
        {
            _context = context;
            _customerMapper = customerMapper;
        }

        public async Task<ActionResult<List<Customer>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            var customers = await _context.Customer.ToListAsync();
            return customers;
        }
        public async Task<Customer> Add(CustomerRequestModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            var res = _customerMapper.MapRequestToCustomer(customer);

            await _context.Customer.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }

            _context.Customer.Remove(account);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> Update(Guid id, CustomerRequestModel requestModel)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                return Guid.Empty;
            }
            account = _customerMapper.MapRequestToSet(account, requestModel);
            _context.Customer.Update(account);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

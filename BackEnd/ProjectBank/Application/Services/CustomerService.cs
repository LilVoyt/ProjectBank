using Castle.Core.Resource;
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
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using System.Net;
using System.Numerics;

namespace ProjectBank.Controller.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDataContext _context;
        private readonly IValidator<Customer> _validator;
        private readonly ICustomerMapper _customerMapper;

        public CustomerService(IDataContext context, IValidator<Customer> validator, ICustomerMapper customerMapper)
        {
            _context = context;
            _validator = validator;
            _customerMapper = customerMapper;
        }

        public async Task<ActionResult<List<CustomerRequestModel>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Customer> customers = _context.Customer;

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(n => n.Name.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Customer, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => customer => customer.Name,
                "lastname" => customer => customer.LastName,
                "country" => customer => customer.Country,
                "phone" => customer => customer.Phone,
                "email" => customer => customer.Email,
                _ => customers => customers.Name
            };

            customers = sortOrder?.ToLower() == "desc"
                ? customers.OrderByDescending(selectorKey)
                : customers.OrderBy(selectorKey);

            List<Customer> customerList = await customers.ToListAsync();

            List<CustomerRequestModel> response = _customerMapper.GetRequestModels(customerList);

            return response;
        }

        public async Task<Customer> Post(CustomerRequestModel customer)
        {
            var res = _customerMapper.GetCustomer(customer);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _context.Customer.AddAsync(res);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Customer> Update(Guid id, CustomerRequestModel requestModel)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            customer = _customerMapper.PutRequestModelInCustomer(customer, requestModel);
            var validationResult = await _validator.ValidateAsync(customer);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            _context.Customer.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> Delete(Guid id)
        {
            var account = await _context.Customer.FindAsync(id);
            if (account == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            _context.Customer.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }
    }
}

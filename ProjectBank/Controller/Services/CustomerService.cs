﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using System.Net;

namespace ProjectBank.Controller.Services
{
    public interface ICustomerService
    {
        Task<ActionResult<List<Account>>> GetAllCustomers();
        Task<Customer> GetCustomer(Guid id);
        Task<Guid> AddCustomer(Customer customer);
        Task<Guid> UpdateCustomer(Guid id);
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
            await _context.SaveChangesAsync();

            return id;
        }
    }
}

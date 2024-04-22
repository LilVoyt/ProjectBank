﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ICustomerService customerService;

        public CustomerController(DataContext context, ICustomerService customerService)
        {
            _context = context;
            this.customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers() //work
        {
            var customers = await customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id) //work
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer) //тут
        {
            await customerService.AddCustomer(customer);

            return Ok(await GetAllCustomers());
        }

        [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id) //Work
    {
        if(id == Guid.Empty)
        {
            return BadRequest();
        }
        await customerService.UpdateCustomer(id);
        return Ok(id);
    }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.DeleteCustomer(id);
            return NoContent();
        }

    }
}

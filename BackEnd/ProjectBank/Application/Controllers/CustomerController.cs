﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

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
        public async Task<ActionResult<List<Customer>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            var customers = await customerService.Get(search, sortItem, sortOrder);

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Post(CustomerRequestModel customer)
        {
            try
            {
                var createdCustomer = await customerService.Post(customer);
                return Ok(createdCustomer);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.Delete(id);   
            
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CustomerRequestModel customer) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.Update(id, customer);
            return Ok(id);
        }

    }
}

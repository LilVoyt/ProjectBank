using Microsoft.AspNetCore.Http;
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
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
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
            var createdCustomer = await customerService.Post(customer);
            return Ok(createdCustomer);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, CustomerRequestModel customer) //Work
        {
            await customerService.Update(id, customer);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await customerService.Delete(id);
            return NoContent();
        }
    }
}

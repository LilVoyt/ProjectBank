using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
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

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers() //work
        {
            var customers = await customerService.GetAllCustomers();

            return Ok(customers);
        }

        [HttpGet("GetCustomer/{id}")]
        public async Task<ActionResult<CustomerRequestModel>> GetCustomer(Guid id) //work
        {

            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var customer = await customerService.GetCustomer(id);

            return Ok(customer);
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerRequestModel customer)
        {
            try
            {
                var createdCustomer = await customerService.AddCustomer(customer);
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


        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.DeleteCustomer(id);   
            
            return NoContent();
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerRequestModel customer) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await customerService.UpdateCustomer(id, customer);
            return Ok(id);
        }

    }
}

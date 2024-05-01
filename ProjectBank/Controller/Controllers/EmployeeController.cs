using Microsoft.AspNetCore.Mvc;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IEmployeeService employeeService;

        public EmployeeController(DataContext context, IEmployeeService employeeService)
        {
            _context = context;
            this.employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees() //work
        {
            var employee = await employeeService.GetAllEmployee();

            return Ok(employee);
        }


        [HttpGet("GetEmployees/{id}")]
        public async Task<ActionResult<EmployeeRequestModel>> GetEmployee(Guid id) //work
        {

            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var employee = await employeeService.GetEmployee(id);

            return Ok(employee);
        }


        [HttpPost("AddEmployee")]
        public async Task<ActionResult<Employee>> AddCustomer(EmployeeRequestModel employee)
        {
            try
            {
                var createdEmployee = await employeeService.AddEmployee(employee);
                return Ok(createdEmployee);
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


        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id) //work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await employeeService.DeleteEmployee(id);

            return NoContent();
        }


        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeRequestModel employee) //Work
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }
            await employeeService.UpdateEmployee(id, employee);
            return Ok(id);
        }
    }
}
        
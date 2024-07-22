using Microsoft.AspNetCore.Mvc;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(DataContext context, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeRequestModel>>> Get(string? search, string? sortItem, string? sortOrder) //work
        {
            var employee = await _employeeService.Get(search, sortItem, sortOrder);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(EmployeeRequestModel employee)
        {
            var createdEmployee = await _employeeService.Post(employee);
            return Ok(createdEmployee);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, EmployeeRequestModel employee) //Work
        {
            await _employeeService.Update(id, employee);
            return Ok(id);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id) //work
        {
            await _employeeService.Delete(id);
            return NoContent();
        }
    }
}

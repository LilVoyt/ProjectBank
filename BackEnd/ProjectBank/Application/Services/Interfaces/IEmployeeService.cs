using Microsoft.AspNetCore.Mvc;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ActionResult<List<EmployeeRequestModel>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Employee> Post(EmployeeRequestModel customer);
        Task<Employee> Update(Guid id, EmployeeRequestModel requestModel);
        Task<Employee> Delete(Guid id);
    }
}

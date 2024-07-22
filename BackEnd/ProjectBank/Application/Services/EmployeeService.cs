using Castle.Core.Resource;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;
using System.Linq.Expressions;

namespace ProjectBank.Controller.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        private readonly EmployeeMapper _employeeMapper;
        private readonly IValidator<Employee> _validator;

        public EmployeeService(DataContext context, IValidator<Employee> validator, EmployeeMapper employeeMapper)
        {
            _context = context;
            _employeeMapper = employeeMapper;
            _validator = validator;
        }


        public async Task<ActionResult<List<EmployeeRequestModel>>> Get(string? search, string? sortItem, string? sortOrder)
        {
            IQueryable<Employee> employees = _context.Employee;

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(n => n.Name.ToLower().Contains(search.ToLower()));
            }

            Expression<Func<Employee, object>> selectorKey = sortItem?.ToLower() switch
            {
                "name" => employees => employees.Name,
                "lastname" => employees => employees.LastName,
                "country" => employees => employees.Country,
                "phone" => employees => employees.Phone,
                "email" => employees => employees.Email,
                _ => employees => employees.Name
            };

            employees = sortOrder?.ToLower() == "desc"
                ? employees.OrderByDescending(selectorKey)
                : employees.OrderBy(selectorKey);

            List<Employee> employeesList = await employees.ToListAsync();

            List<EmployeeRequestModel> response = _employeeMapper.GetRequestModels(employeesList);

            return response;
        }

        public async Task<Employee> Post(EmployeeRequestModel employee)
        {
            var res = _employeeMapper.GetEmployee(employee);

            var validationResult = await _validator.ValidateAsync(res);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }

            await _context.Employee.AddAsync(res);
            await _context.SaveChangesAsync();
            return res;
        }

        public async Task<Employee> Update(Guid id, EmployeeRequestModel requestModel)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }
            employee = _employeeMapper.PutRequestModelInEmployee(employee, requestModel);
            var validationResult = await _validator.ValidateAsync(employee);
            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errorMessages);
            }
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> Delete(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Account with ID {id} not found.");
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }
    }
}

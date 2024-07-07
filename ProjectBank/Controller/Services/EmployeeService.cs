using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Controller.Services
{
    public interface IEmployeeService
    {
        Task<ActionResult<List<Employee>>> GetAllEmployee();
        Task<EmployeeRequestModel> GetEmployee(Guid id);
        Task<Employee> AddEmployee(EmployeeRequestModel customer);
        Task<Guid> UpdateEmployee(Guid id, EmployeeRequestModel requestModel);
        Task<Guid> DeleteEmployee(Guid id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;

        public EmployeeService(DataContext context)
        {
            _context = context;
        }


        public async Task<ActionResult<List<Employee>>> GetAllEmployee()
        {
            var employee = await _context.Employee.ToListAsync();

            return employee;
        }


        public async Task<EmployeeRequestModel> GetEmployee(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return null;
            }
            var res = MapRequestToDB(employee);

            return res;
        }


        public async Task<Employee> AddEmployee(EmployeeRequestModel employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            var res = MapRequestToEmployee(employee);

            _context.Employee.AddAsync(res);
            await _context.SaveChangesAsync();

            return res;
        }


        public async Task<Guid> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return Guid.Empty;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return id;
        }


        public async Task<Guid> UpdateEmployee(Guid id, EmployeeRequestModel requestModel)//need changes
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return Guid.Empty;
            }
            employee = MapRequestToSet(employee, requestModel);
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();

            return id;
        }


        private Employee MapRequestToEmployee(EmployeeRequestModel requestModel)
        {
            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.Name = requestModel.Name;
            employee.LastName = requestModel.LastName;
            employee.Country = requestModel.Country;
            employee.Phone = requestModel.Phone;
            employee.Email = requestModel.Email;

            return employee;
        }


        private EmployeeRequestModel MapRequestToDB(Employee employee)
        {
            var requestModel = new EmployeeRequestModel();
            requestModel.Name = employee.Name;
            requestModel.LastName = employee.LastName;
            requestModel.Country = employee.Country;
            requestModel.Phone = employee.Phone;
            requestModel.Email = employee.Email;

            return requestModel;
        }


        private Employee MapRequestToSet(Employee res, EmployeeRequestModel employee)
        {
            res.Name = employee.Name;
            res.LastName = employee.LastName;
            res.Country = employee.Country;
            res.Phone = employee.Phone;
            res.Email = employee.Email;

            return res;
        }
    }
}

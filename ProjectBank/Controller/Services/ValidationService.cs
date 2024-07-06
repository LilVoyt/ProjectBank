using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;

namespace ProjectBank.Controller.Services
{
    public interface IValidationService
    {
        Task<bool> IsCustomerNotExists(Guid customerID);
        Task<bool> IsEmployeeNotExists(Guid employeeID);
        Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerID);
        Task<bool> IsNameUnique(string name);
    }
    public class ValidationService : IValidationService
    {
        private readonly DataContext _context;

        public ValidationService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> IsCustomerNotExists(Guid customerId)
        {
            return await _context.Customers.AnyAsync(e => e.Id == customerId);
        }

        public async Task<bool> IsEmployeeNotExists(Guid employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.Id == employeeId);
        }

        public async Task<bool> IsNotAlreadyRegisteredCustomer(Guid customerId)
        {
            return !await _context.Accounts.AnyAsync(a => a.CustomerID == customerId);
        }

        public async Task<bool> IsNameUnique(string name)
        {
            return !await _context.Accounts.AnyAsync(a => a.Name == name);
        }
    }

}
